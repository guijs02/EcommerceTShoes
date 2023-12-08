using EcommerceCartAPI.Domain.Models;

namespace EcommerceCartAPI.Infraestructure.Repository
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoDeCompra> AddCart(Produto produto, string userId);
        Task<List<CarrinhoDeCompra>> GetAllCarrinho(string userId);
        Task<CarrinhoDeCompra> EditCarrinho(Produto produto);
        Task<bool> DeleteItemCarrinho(int id);
        Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinho(int id);
        Task<bool> ClearCart();
        Task<bool> EditProdutoCarrinhoQuantidade(CarrinhoDeCompra carrinho, string userId);
    }
}
