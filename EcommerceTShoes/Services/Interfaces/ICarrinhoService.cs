using EcommerceWeb.Model;

namespace EcommerceAPI.Services.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoDeCompra> AddCart(Produto produto, string UserId = "");
        Task<List<CarrinhoDeCompra>> GetAllCarrinho();
        Task<bool> DeleteItemCarrinho(int id);
        Task<CarrinhoDeCompra> EditCarrinho(Produto produto);
        Task<Produto> GetByIdProdutoCarrinho(int id);
    }
}
