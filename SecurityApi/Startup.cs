using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Security.Application.Command;
using Security.Application.Port;
using Security.Application.SeedWork;
using Security.Application.Validator;
using Security.Common.Configuration;
using Security.Common.Converter;
using Security.Infraestructure.Adapter.Security;
using Security.Infraestructure.Adapter.Service;
using Security.Infraestructure.Adapter.SQLServer.Adapter;
using Security.Infraestructure.Adapter.SQLServer.Repository;
using Security.Infraestructure.Converter;
using Security.Infraestructure.Entity;
using Security.Model.Model;
using SecurityApi.Exception;
using System.Reflection;

namespace SecurityApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SecurityApi", Version = "v1" });
            });
            services
               .AddMvc()
               .AddJsonOptions(options =>
               {
                   //Camelcase
                   options.JsonSerializerOptions.PropertyNamingPolicy = null;
               })
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
               .AddControllersAsServices();
            ConfigureExternalLibraries(services);
            ConfigureServiceIfraestructure(services);
        }
        private void ConfigureExternalLibraries(IServiceCollection services)
        {
            //Add MediatR configuration
            services.AddMediatR(typeof(AuthenticateCommand).GetTypeInfo().Assembly);

            //Add Validators
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AuthenticateCommandValidator>());

            //Add interceptor validations
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }

        private void ConfigureServiceIfraestructure(IServiceCollection services)
        {
            //Configuración
            RepositorySettings oRepositorySettings = new RepositorySettings()
                                     .SetConnectionString(Configuration.GetSection("ConnectionStrings:SQLServer").Value);
            services.AddSingleton<IRepositorySettings>(oRepositorySettings);

            GeneralSettings oGeneralConfiguration = new GeneralSettings().SetKeyPassword(Configuration.GetSection("Security:KeyPassword").Value)
                                                                         .SetKeyToken(Configuration.GetSection("Security:KeyToken").Value)
                                                                         .SetTimeToken(int.Parse(Configuration.GetSection("Security:TimeToken").Value));


            services.AddSingleton<IGeneralSettings>(oGeneralConfiguration);

            //Converter
            services.AddSingleton(typeof(IEntityConverter<User, SecurityUserEntity>), typeof(SecurityUserConverter));
            
            //Repositories - adapter
            services.AddScoped<ISecurityUser, SecurityUserRepository>();
            services.AddScoped<ISecurityPort, SecurityAdapter>();
            services.AddScoped<IUserManagerPort, SecurityUserAdapter>();

            services.AddScoped<ISecurityService, SecurityService>();
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecurityApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.ConfigureExceptionHandler();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
