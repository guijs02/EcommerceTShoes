using Azure;
using EcommerceCartAPI.Context;
using EcommerceCartAPI.Interfaces;
using EcommerceCartAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EcommerceCartAPI.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly SQLServerContext _db;
        private readonly HttpClient _client;
        private const string API_PRODUTO = "https://localhost:7064/api/TShoes";

        public CarrinhoRepository(SQLServerContext db, HttpClient client)
        {
            _db = db;
            _client = client;
        }
        public async Task<CarrinhoDeCompra> AddCart(Produto produto, string userId)
        {
            
            CarrinhoDeCompra carrinho = new()
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
        public async Task<List<CarrinhoDeCompra>> GetAllCarrinho(string userId)
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

        public async Task<CarrinhoDeCompra> EditCarrinho(Produto produto)
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

        public async Task<Produto> GetByIdProdutoCarrinho(int id)
        {
            var response = await _client.GetAsync(API_PRODUTO);
            var json = await response.Content.ReadAsStringAsync();
            //var produtos = JsonSerializer.Deserialize<List<ProdutoDto>>(json, new JsonSerializerOptions() { WriteIndented = true, PropertyNameCaseInsensitive = true }) ;

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

            var Produto = new Produto
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
