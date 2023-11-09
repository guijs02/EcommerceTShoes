using EcommerceCartAPI.Interfaces;
using EcommerceCartAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepository _repo;
        public CarrinhoController(ICarrinhoRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(Produto produtoObj)
        {
            try
            {
                var UserId = GetUserId();

                var produto = await _repo.AddCart(produtoObj, UserId);
                if (produto is null)
                    return NotFound();

                return Ok(produto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var UserId = GetUserId();
                var produtos = await _repo.GetAllCarrinho(UserId);

                if (produtos is null)
                    return NotFound();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> EditCarrinho(Produto produto)
        {
            try
            {
                var produtos = await _repo.EditCarrinho(produto);
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
        public async Task<IActionResult> GetByIdProdutoCarrinho(int id)
        {
            try
            {
                var produtos = await _repo.GetByIdProdutoCarrinho(id);
                if (produtos is null)
                    return NotFound();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItemCarrinho(int id)
        {
            try
            {
                var apagado = await _repo.DeleteItemCarrinho(id);
                if (!apagado)
                    return BadRequest();

                return Ok(apagado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GetUserId()
        {
            var UserId = HttpContext.User.Claims.Where(c => c.Type.ToString() == "id")?.FirstOrDefault()?.Value;

            return UserId;

        }
    }
}
