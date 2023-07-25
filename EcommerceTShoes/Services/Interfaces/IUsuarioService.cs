using LoginAPI.Dto;

namespace EcommerceTShoes.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task Cadastro(CreateUsuarioDto dto);
        Task Login(LoginUsuarioDto dto);
    }
}