using EcommerceAPI.Repository.Interfaces;
using LoginAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        public readonly IUsuarioRepository _repo;
        public UsuarioController(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastro(CreateUsuarioDto dto)
        {
            try
            {
                var result = await _repo.Cadastro(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            try
            {
               var token =  await _repo.Login(dto);
                
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
                //throw ex;
            }
        }
    }
}
