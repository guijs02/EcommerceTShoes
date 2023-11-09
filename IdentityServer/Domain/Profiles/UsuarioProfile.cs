using AutoMapper;
using IdentityServer;
using IdentityServer.Models;

namespace IdentityServer.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
