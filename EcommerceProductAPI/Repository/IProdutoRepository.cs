using EcommerceProductAPI.Models;

namespace EcommerceProductAPI.Repository
{
    public interface IProdutoRepository
    {
        Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero);
        Task<Produto> GetProduto(int id);
        Task<List<ProdutoDto>> GetAllProdutos();
    }
}
