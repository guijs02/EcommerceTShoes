using EcommerceWeb.Dto;
using EcommerceWeb.Model;

namespace EcommerceAPI.Services.Interfaces
{
    public interface ITShoesService
    {
        Task<List<ProdutoDto>> GetAllProdutos();
        Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero);
        Task<Produto> GetProduto(int id);
        //Task<List<Produto>> GetAllProdutosMasculino();
    }
}
