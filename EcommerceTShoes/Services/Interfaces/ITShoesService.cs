using EcommerceTShoes.Model;

namespace EcommerceAPI.Services.Interfaces
{
    public interface ITShoesService
    {
        Task<List<Produto>> GetAllProdutos();
        Task<List<Produto>> GetProdutosByGenero(int idgenero);
        Task<Produto> GetProduto(int id);
        //Task<List<Produto>> GetAllProdutosMasculino();
    }
}
