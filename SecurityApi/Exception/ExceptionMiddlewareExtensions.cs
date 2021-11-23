using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Security.Common.Exception;
using System.Net;

namespace SecurityApi.Exception
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            _ = app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is CustomErrorException)
                        {
                            var lstErrors = ((CustomErrorException)contextFeature.Error).GetListError();
                            if (lstErrors.Count > 0)
                            {
                                context.Response.StatusCode = lstErrors[0].StatusCode.GetHashCode();
                            }
                            else
                            {
                                context.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                            }

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(lstErrors));
                        }
                        else
                        {
                            context.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                        }

                    }
                });
            });
        }

    }
}
