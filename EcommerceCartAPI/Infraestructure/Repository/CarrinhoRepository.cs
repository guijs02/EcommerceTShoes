using EcommerceCartAPI.Domain.Models;
using EcommerceCartAPI.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceCartAPI.Infraestructure.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly SQLServerContext _db;

        public CarrinhoRepository(SQLServerContext db)
        {
            _db = db;
        }
        public async Task<CarrinhoDeCompra> AddCart(Produto produto, string userId)
        {
            var produtos = RetornarProdutosRepetidosNoBanco(produto, userId);

            if (produtos is not null)
                return produtos;

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
            var list = await _db.Carrinho.Where(c => c.UserId == userId).ToListAsync();
            return list;
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
        public async Task<bool> EditProdutoCarrinhoQuantidade(CarrinhoDeCompra carrinho, string userId)
        {
            if (carrinho.Quantidade == 0)
            {
                await DeleteItemCarrinho(carrinho.Id);
                return false;
            }
            var carrinhoDb = await _db.Carrinho.FirstAsync(c => c.Id == carrinho.Id);
            carrinhoDb.Quantidade = carrinho.Quantidade;

            _db.Carrinho.Update(carrinhoDb);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinho(int id)
        {

            var produtoCarrinho = await (from carrinho in _db.Carrinho
                                         where carrinho.ProdutoId == id
                                         select carrinho
                                        ).FirstOrDefaultAsync();

            var ProdutoCarrinho = new ProdutoCarrinhoDto
            {
                Id = produtoCarrinho.ProdutoId,
                Descricao = produtoCarrinho.Descricao,
                ImagemUrl = produtoCarrinho.ImagemUrl,
                Nome = produtoCarrinho.Nome,
                Preco = produtoCarrinho.Preco,
                Tamanho = produtoCarrinho.Tamanho,

            };

            return ProdutoCarrinho;
        }

        public async Task<bool> ClearCart()
        {
            var produtos = _db.Carrinho.Select(s => s);
            _db.Carrinho.RemoveRange(produtos);
            await _db.SaveChangesAsync();
            return true;
        }
        private CarrinhoDeCompra? RetornarProdutosRepetidosNoBanco(Produto produto, string userId)
        {
            var produtosRepetidosNoCarrinho = _db.Carrinho.Where(c => c.UserId == userId &&
                                                                c.ProdutoId == produto.Id &&
                                                                c.Tamanho == produto.Tamanho)
                                                               .AsNoTracking();
            if (produtosRepetidosNoCarrinho.Any())
            {
                var produtoJaInserido = produtosRepetidosNoCarrinho.First();
                produtoJaInserido.Quantidade = produtosRepetidosNoCarrinho.First().Quantidade + 1;

                _db.Carrinho.Update(produtoJaInserido);
                _db.SaveChanges();
                return produtoJaInserido;
            }
            return null;
        }
    }
}
