using EcommerceWeb.Dto;
using EcommerceWeb.Model;

namespace EcommerceAPI.Services.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoDeCompraViewModel> AddCart(ProdutoViewModel produto, string UserId = "");
        Task<List<CarrinhoDeCompraViewModel>> GetAllCarrinho();
        Task<bool> DeleteItemCarrinho(int id);
        Task<CarrinhoDeCompraViewModel> EditCarrinho(ProdutoViewModel produto);
        Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinho(int id);
        Task<bool> Checkout(OrderDetails orderDetails);
        Task<bool> EditCarrinhoQuantidade(CarrinhoDeCompraViewModel carrinho);
    }
}
