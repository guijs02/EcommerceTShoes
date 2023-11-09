using EcommerceProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProductAPI.Controllers
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

        public async Task<IActionResult> GetProdutosByGenero(int idgenero)
        {
            try
            {
                var produtos = await _repo.GetProdutosByGenero(idgenero);
                if (produtos is null)
                    return NotFound();
                
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrdutos()
        {
            try
            {
                var produtos = await _repo.GetAllProdutos();
                if (produtos is null)
                    return NotFound();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduto(int id)
        {
            try
            {
                var produtos = await _repo.GetProduto(id);
                if (produtos is null)
                    return NotFound();
                
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}