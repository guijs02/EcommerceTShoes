using LoginAPI.Dto;

namespace EcommerceAPI.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> Cadastro(CreateUsuarioDto dto);
        Task<string> Login(LoginUsuarioDto dto);
    }
}
