using EcommerceWeb.Dto;
using EcommerceWeb.Model;

namespace EcommerceAPI.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<List<ProdutoDto>> GetAllProdutos();
        Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero);
        Task<ProdutoViewModel> GetProduto(int id);
        //Task<List<Produto>> GetAllProdutosMasculino();
    }
}
