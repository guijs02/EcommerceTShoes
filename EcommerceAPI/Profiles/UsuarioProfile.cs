using AutoMapper;
using LoginAPI.Dto;
using LoginAPI.Models;

namespace EcommerceAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
