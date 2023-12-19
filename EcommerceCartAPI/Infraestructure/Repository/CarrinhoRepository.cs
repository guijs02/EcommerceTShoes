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

        public async Task<List<CarrinhoDeCompra>> GetAllCarrinhoAsync(string userId)
        {
            var list = await _db.Carrinho.Where(c => c.UserId == userId).ToListAsync();
            return list;
        }
        public async Task<bool> DeleteItemCarrinhoAsync(int id)
        {
            var a = _db.Carrinho.Where(x => x.Id == id)
                        .ExecuteDelete();
            _db.SaveChanges();
            return true;
        }

        public async Task<CarrinhoDeCompra> EditCarrinhoAsync(Produto produto)
        {
            var carrinho = await _db.Carrinho.Where(w => w.ProdutoId == produto.Id).FirstOrDefaultAsync();

            if (carrinho is not null)
            {
                carrinho.Nome = produto.Nome;
                carrinho.Tamanho = produto.Tamanho;
                carrinho.ImagemUrl = produto.ImagemUrl;
                carrinho.Preco = produto.Preco;
                carrinho.Descricao = produto.Descricao;
                _db.Carrinho.Update(carrinho);
                await _db.SaveChangesAsync();
            }

            return carrinho;
        }
        public async Task<bool> EditProdutoCarrinhoQuantidadeAsync(CarrinhoDeCompra carrinho, string userId)
        {
            var carrinhoDb = await _db.Carrinho.FirstAsync(c => c.Id == carrinho.Id);

            if (carrinhoDb is null)
                return false;

            carrinhoDb.Quantidade = carrinho.Quantidade;

            _db.Carrinho.Update(carrinhoDb);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<ProdutoCarrinhoDto> GetByIdProdutoCarrinhoAsync(int id)
        {

            var produtoCarrinho = await (from carrinho in _db.Carrinho
                                         where carrinho.ProdutoId == id
                                         select carrinho
                                        ).FirstOrDefaultAsync();

            if (produtoCarrinho is null)
            {
                return null;
            }

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

        public async Task<bool> ClearCartAsync()
        {
            var produtos = _db.Carrinho.Select(s => s).ToList();
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
