using LoginAPI.Dto;

namespace EcommerceTShoes.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> Cadastro(CreateUsuarioDto dto);
        Task Login(LoginUsuarioDto dto);
        Task Logout();
    }
}