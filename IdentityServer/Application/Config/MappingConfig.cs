using AutoMapper;
using IdentityServer;
using IdentityServer.Models;

namespace IdentityServerAPI.Application.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateUsuarioDto, Usuario>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
