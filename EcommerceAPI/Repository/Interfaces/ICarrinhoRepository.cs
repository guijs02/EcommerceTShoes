using EcommerceWeb.Model;

namespace EcommerceAPI.Repository.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoDeCompraViewModel> AddCart(ProdutoViewModel produto,string userId);
        Task<List<CarrinhoDeCompraViewModel>> GetAllCarrinho(string userId);
        Task<CarrinhoDeCompraViewModel> EditCarrinho(ProdutoViewModel produto);
        Task<bool> DeleteItemCarrinho(int id);
        ProdutoViewModel GetByIdProdutoCarrinho(int id);
    }
}
