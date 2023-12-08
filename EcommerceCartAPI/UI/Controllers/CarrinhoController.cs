using EcommerceCartAPI.Application.RabbitMQSender;
using EcommerceCartAPI.Domain.Messages;
using EcommerceCartAPI.Domain.Models;
using EcommerceCartAPI.Infraestructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceCartAPI.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepository _repo;
        private readonly IRabbitMQMessageSender _messageSender;
        public CarrinhoController(ICarrinhoRepository repo, IRabbitMQMessageSender messageSender)
        {
            _repo = repo;
            _messageSender = messageSender;
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(Produto produtoObj)
        {
            try
            {
                var UserId = GetUserIdAsync();

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
                var UserId = GetUserIdAsync();
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
        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout(CheckoutMessage checkoutMessage)
        {
            try
            {
                if (checkoutMessage is null)
                    return NotFound();

                var result = checkoutMessage.Cart.Where(f => f.UserId is null).Any();

                if (result)
                {
                    var userId = GetUserIdAsync();
                    checkoutMessage.Cart.ForEach(f => f.UserId = userId);
                }

                _messageSender.SendMessage(checkoutMessage, "checkoutqueue");

                await _repo.ClearCart();

                return Ok(true);
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
        [HttpPut]
        [Route("editProductDetails")]
        public async Task<IActionResult> EditQuantidade(CarrinhoDeCompra carrinho)
        {
            try
            {
                var userId = GetUserIdAsync();
                var editou = await _repo.EditProdutoCarrinhoQuantidade(carrinho, userId);

                return Ok(editou);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GetUserIdAsync()
        {
            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return UserId;

        }

    }
}
