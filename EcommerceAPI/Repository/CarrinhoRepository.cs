using EcommerceAPI.Repository.Interfaces;
using EcommerceWeb.Auth;
using EcommerceWeb.Dto;
using EcommerceWeb.Model;
using GeekShopping.ProductAPI.Context;
using LoginAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly SQLServerContext _db;
        public CarrinhoRepository(SQLServerContext db)
        {
            _db = db;
            
        }
        public async Task<CarrinhoDeCompraViewModel> AddCart(ProdutoViewModel produto, string userId)
        {
            
            CarrinhoDeCompraViewModel carrinho = new()
            {
                Descricao = produto.Descricao,
                ImagemUrl = produto.ImagemUrl,
                Nome = produto.Nome,
                Preco = produto.Preco,
                ProdutoId = produto.Id,
                Tamanho = produto.Tamanho,
                UserId = userId
                
            };

            await _db.Carrinho.AddAsync(carrinho);
            await _db.SaveChangesAsync();

            return carrinho;

        }
        public async Task<List<CarrinhoDeCompraViewModel>> GetAllCarrinho(string userId)
        {
            return await _db.Carrinho.Where(c => c.UserId == userId).ToListAsync();
        }
        public async Task<bool> DeleteItemCarrinho(int id)
        {
            var produto = await _db.Carrinho.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("O obj é nulo"); 
            
            _db.Carrinho.Remove(produto);
            _db.SaveChanges();
            return true;
        }

        public async Task<CarrinhoDeCompraViewModel> EditCarrinho(ProdutoViewModel produto)
        {
            var carrinho = _db.Carrinho.Where(w => w.ProdutoId == produto.Id).FirstOrDefault() ?? throw new Exception("carrinho é nulo"); 
     
            carrinho.Nome = produto.Nome;
            carrinho.Tamanho = produto.Tamanho;
            carrinho.ImagemUrl = produto.ImagemUrl;
            carrinho.Preco = produto.Preco;
            carrinho.Descricao = produto.Descricao;
            

            _db.Carrinho.Update(carrinho);
            await _db.SaveChangesAsync();
            return carrinho;
        }

        public ProdutoViewModel GetByIdProdutoCarrinho(int id)
        {
            var query = (from carrinho in _db.Carrinho
                         from produto in _db.Products
                         where carrinho.ProdutoId == id && produto.Id == id
                         select new
                         {
                             produto,
                             carrinho.Tamanho,
                         }).AsNoTracking() ?? 
                         throw new NullReferenceException("O query é nulo"); 
            var result = query.First();
            //result.produto.
            var Produto = new ProdutoViewModel
            {
                Id = result.produto.Id,
                Descricao = result.produto.Descricao,
                Genero = result.produto.Genero,
                ImagemUrl = result.produto.ImagemUrl,
                Nome = result.produto.Nome,
                Preco = result.produto.Preco,
                Tamanho = result.Tamanho
            };

            return Produto;
        }
     
    }
}
