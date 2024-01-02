using EcommerceCartAPI.Application.RabbitMQSender;
using EcommerceCartAPI.Domain.Messages;
using EcommerceCartAPI.Domain.Models;
using EcommerceCartAPI.Infraestructure.Repository;
using ErrorMessagesApis;
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
        public async Task<IActionResult> AddCartAsync(Produto produtoObj)
        {
            try
            {
                var UserId = GetUserById();

                var produto = await _repo.AddCart(produtoObj, UserId);

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var UserId = GetUserById();
                var produtos = await _repo.GetAllCarrinhoAsync(UserId);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }
        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> CheckoutAsync(CheckoutMessage checkoutMessage)
        {
            try
            {
                var result = checkoutMessage.Cart.Where(f => f.UserId is null).Any();

                if (result)
                {
                    var userId = GetUserById();
                    checkoutMessage.Cart.ForEach(f => f.UserId = userId);
                }

                _messageSender.SendMessage(checkoutMessage, "checkoutqueue");

                await _repo.ClearCartAsync();

                return Accepted(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }
        [HttpPut]
        public async Task<IActionResult> EditCarrinhoAsync(Produto produto)
        {
            try
            {
                var produtos = await _repo.EditCarrinhoAsync(produto);
                if (produtos is null)
                    return NotFound(NotFoundErrorMessages.ProdutoNull);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdProdutoCarrinhoAsync(int id)
        {
            try
            {
                var produtos = await _repo.GetByIdProdutoCarrinhoAsync(id);
                if (produtos is null)
                    return NotFound(NotFoundErrorMessages.ProdutoNull);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteItemCarrinhoAsync(int id)
        {
            try
            {
                var apagado = await _repo.DeleteItemCarrinhoAsync(id);
                if (!apagado)
                    return NotFound(NotFoundErrorMessages.ProdutoNull);

                return Ok(apagado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }
        [HttpPut]
        [Route("editProductDetails")]
        public async Task<IActionResult> EditQuantidadeAsync(CarrinhoDeCompra carrinho)
        {
            try
            {
                bool result;
                var userId = GetUserById();

                if (carrinho.Quantidade == 0)
                {
                    result = await _repo.DeleteItemCarrinhoAsync(carrinho.Id);
                }
                else
                {
                    result = await _repo.EditProdutoCarrinhoQuantidadeAsync(carrinho, userId);
                }
                if (!result)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }
        private string? GetUserById()
        {
            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return UserId;

        }

    }
}
