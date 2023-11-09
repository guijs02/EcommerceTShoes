using EcommerceAPI.Repository.Interfaces;
using EcommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepository _repo;
        public CarrinhoController(ICarrinhoRepository repo, HttpClient http)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(ProdutoViewModel produtoObj)
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
        public async Task<IActionResult> EditCarrinho(ProdutoViewModel produto)
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
        public IActionResult GetByIdProdutoCarrinho(int id)
        {
            try
            {
                var produtos = _repo.GetByIdProdutoCarrinho(id);
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
