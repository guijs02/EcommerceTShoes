using EcommerceProductAPI.Domain.Models;

namespace EcommerceProductAPI.Infraestructure.Repository
{
    public interface IProdutoRepository
    {
        Task<List<ProdutoDto>> GetProdutosByGeneroAsync(int idgenero);
        Task<Produto> GetProdutoAsync(int id);
        Task<List<ProdutoDto>> GetAllPrdutosAsync();
    }
}
