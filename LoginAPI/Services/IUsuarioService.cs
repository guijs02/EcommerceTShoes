using LoginAPI.Dto;

namespace LoginAPI.Services
{
    public interface IUsuarioService
    {
        Task Cadastro(CreateUsuarioDto dto);
        Task Login(LoginUsuarioDto dto);
    }
}
