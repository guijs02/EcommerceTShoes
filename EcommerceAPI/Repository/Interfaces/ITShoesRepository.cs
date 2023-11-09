using EcommerceWeb.Dto;
using EcommerceWeb.Model;

namespace EcommerceAPI.Repository.Interfaces
{
    public interface ITShoesRepository
    {
        Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero);
        Task<Produto> GetProduto(int id);
        Task<List<ProdutoDto>> GetAllProdutos();
    }
}
