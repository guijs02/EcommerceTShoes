using EcommerceWeb.Model;

namespace EcommerceAPI.Repository.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoDeCompra> AddCart(Produto produto,string userId);
        Task<List<CarrinhoDeCompra>> GetAllCarrinho(string userId);
        Task<CarrinhoDeCompra> EditCarrinho(Produto produto);
        Task<bool> DeleteItemCarrinho(int id);
        Produto GetByIdProdutoCarrinho(int id);
    }
}
