using EcommerceTShoes.Model;

namespace EcommerceAPI.Repository.Interfaces
{
    public interface ITShoesRepository
    {
        Task<List<Produto>> GetProdutosByGenero(int idgenero);
        Task<Produto> GetProduto(int id);
        Task<List<Produto>> GetAllProdutos();
    }
}
