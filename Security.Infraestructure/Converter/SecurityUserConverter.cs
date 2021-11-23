using AutoMapper;
using Security.Common.Converter;
using Security.Infraestructure.Entity;
using Security.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Infraestructure.Converter
{
    public class SecurityUserConverter : IEntityConverter<User, SecurityUserEntity>
    {
        private readonly IMapper _Mapper;
        public SecurityUserConverter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, SecurityUserEntity>()
                 .ForMember(dest => dest.SecurityUserId, model => model.MapFrom(m => m.Id))
                .ReverseMap();
            });
            _Mapper = new Mapper(config);
        }
        public User FromEntityToModel(SecurityUserEntity source)
        {
            return _Mapper.Map<User>(source);
        }

        public SecurityUserEntity FromModelToEntity(User source)
        {
            return _Mapper.Map<SecurityUserEntity>(source);
        }
    }
}
