using EcommerceCartAPI.Domain.Models;

namespace EcommerceCartAPI.Infraestructure.Repository
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoDeCompra> AddCart(Produto produto, string userId);
        Task<List<CarrinhoDeCompra>> GetAllCarrinhoAsync(string userId);
        Task<CarrinhoDeCompra> EditCarrinhoAsync(Produto produto);
        Task<bool> DeleteItemCarrinhoAsync(int id);
        Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinhoAsync(int id);
        Task<bool> ClearCartAsync();
        Task<bool> EditProdutoCarrinhoQuantidadeAsync(CarrinhoDeCompra carrinho, string userId);
    }
}
