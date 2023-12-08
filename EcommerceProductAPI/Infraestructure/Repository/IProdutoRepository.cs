using EcommerceProductAPI.Domain.Models;

namespace EcommerceProductAPI.Infraestructure.Repository
{
    public interface IProdutoRepository
    {
        Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero);
        Task<Produto> GetProduto(int id);
        Task<List<ProdutoDto>> GetAllProdutos();
    }
}
