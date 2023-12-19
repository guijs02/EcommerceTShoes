using EcommerceProductAPI.Infraestructure.Repository;
using ErrorMessagesApis;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProductAPI.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repo;
        public ProdutoController(IProdutoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetByGenero/{idgenero:int}")]

        public async Task<IActionResult> GetProdutosByGeneroAsync(int idgenero)
        {
            try
            {
                var produtos = await _repo.GetProdutosByGeneroAsync(idgenero);
                if (!produtos.Any())
                    return NotFound(NotFoundErrorMessages.GeneroNull);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrdutosAsync()
        {
            try
            {
                var produtos = await _repo.GetAllPrdutosAsync();
                if (!produtos.Any())
                    return NoContent();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProdutoAsync(int id)
        {
            try
            {
                var produtos = await _repo.GetProdutoAsync(id);
                if (produtos is null)
                    return NotFound(NotFoundErrorMessages.ProdutoNull);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
    }
}