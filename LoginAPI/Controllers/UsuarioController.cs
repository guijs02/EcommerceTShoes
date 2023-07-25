using LoginAPI.Dto;
using LoginAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        public IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto dto)
        {
            try
            {
                await _usuarioService.Cadastro(dto);
                return Ok("Cadastrado com sucesso");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            try
            {
                await _usuarioService.Login(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}