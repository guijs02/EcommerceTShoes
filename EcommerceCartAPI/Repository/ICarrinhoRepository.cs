using EcommerceCartAPI.Models;

namespace EcommerceCartAPI.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoDeCompra> AddCart(Produto produto, string userId);
        Task<List<CarrinhoDeCompra>> GetAllCarrinho(string userId);
        Task<CarrinhoDeCompra> EditCarrinho(Produto produto);
        Task<bool> DeleteItemCarrinho(int id);
        Task<Produto> GetByIdProdutoCarrinho(int id);
    }
}
