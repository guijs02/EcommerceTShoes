using EcommerceAPI.Repository;
using EcommerceAPI.Repository.Interfaces;
using EcommerceTShoes.Model;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TShoesController : ControllerBase
    {
        private readonly ITShoesRepository _repo;
        public TShoesController(ITShoesRepository repo)
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