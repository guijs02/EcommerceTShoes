using EcommerceAPI.Repository.Interfaces;
using EcommerceTShoes.Model;
using GeekShopping.ProductAPI.Context;
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
        public async Task<CarrinhoDeCompra> AddCart(Produto produto)
        {

            CarrinhoDeCompra carrinho = new CarrinhoDeCompra()
            {
                Descricao = produto.Descricao,
                ImagemUrl = produto.ImagemUrl,
                Nome = produto.Nome,
                Preco = produto.Preco,
                ProdutoId = produto.Id,
                Tamanho = produto.Tamanho,
            };

            await _db.Carrinho.AddAsync(carrinho);
            await _db.SaveChangesAsync();

            return carrinho;

        }
        public async Task<List<CarrinhoDeCompra>> GetAllCarrinho()
        {
            return await _db.Carrinho.ToListAsync();

        }
        public async Task<bool> DeleteItemCarrinho(int id)
        {
            var produto = await _db.Carrinho.FirstOrDefaultAsync(x => x.Id == id);
            if (produto is null)
            {
                throw new Exception("O obj é nulo");
            }
            _db.Carrinho.Remove(produto);
            _db.SaveChanges();
            return true;
        }

        public async Task<CarrinhoDeCompra> EditCarrinho(Produto produto)
        {
            var carrinho = _db.Carrinho.Where(w => w.ProdutoId == produto.Id).FirstOrDefault();
            if (carrinho is null)
            {
                throw new Exception("carrinho é nulo");
            }

            carrinho.Nome = produto.Nome;
            carrinho.Tamanho = produto.Tamanho;
            carrinho.ImagemUrl = produto.ImagemUrl;
            carrinho.Preco = produto.Preco;
            carrinho.Descricao = produto.Descricao;

            _db.Carrinho.Update(carrinho);
            await _db.SaveChangesAsync();
            return carrinho;
        }

        public Produto GetByIdProdutoCarrinho(int id)
        {
            var query = (from carrinho in _db.Carrinho
                         from produto in _db.Products
                         where carrinho.ProdutoId == id && produto.Id == id
                         select new
                         {
                             produto,
                             carrinho.Tamanho,
                         }).AsNoTracking();

            if (query is null)
                throw new NullReferenceException("O query é nulo");

            var Produto = new Produto
            {
                Id = query.First().produto.Id,
                Descricao = query.First().produto.Descricao,
                Genero = query.First().produto.Genero,
                ImagemUrl = query.First().produto.ImagemUrl,
                Nome = query.First().produto.Nome,
                Preco = query.First().produto.Preco,
                Tamanho = query.First().Tamanho
            };

            return Produto;
        }
    }
}
