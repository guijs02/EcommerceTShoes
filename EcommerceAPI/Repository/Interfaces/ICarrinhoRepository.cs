using EcommerceTShoes.Model;

namespace EcommerceAPI.Repository.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoDeCompra> AddCart(Produto produto);
        Task<List<CarrinhoDeCompra>> GetAllCarrinho();
        Task<CarrinhoDeCompra> EditCarrinho(Produto produto);
        Task<bool> DeleteItemCarrinho(int id);
        Produto GetByIdProdutoCarrinho(int id);
    }
}
