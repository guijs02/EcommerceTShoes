using EcommerceProductAPI.Domain.Models;
using EcommerceProductAPI.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EcommerceProductAPI.Infraestructure.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly SQLServerContext _db;
        public ProdutoRepository(SQLServerContext db)
        {
            _db = db;
        }
        public async Task<List<ProdutoDto>> GetProdutosByGenero(int idgenero)
        {
            return idgenero switch
            {
                1 => await GetListByGenero(idgenero),
                2 => await GetListByGenero(idgenero),
                _ => throw new Exception("Não há esse genero no banco de dados")
            };
        }
        public async Task<Produto> GetProduto(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<List<ProdutoDto>> GetAllProdutos()
        {
            var query = (from p in _db.Products
                         select new ProdutoDto
                         {
                             Id = p.Id,
                             ImageUrl = p.ImagemUrl,
                             Nome = p.Nome,
                             Preco = p.Preco

                         }).AsQueryable();

            return await query.ToListAsync();
        }

        private async Task<List<ProdutoDto>> GetListByGenero(int idgenero)
        {
            return await (from p in _db.Products
                          where (int)p.Genero == idgenero
                          select new ProdutoDto
                          {
                              Preco = p.Preco,
                              Id = p.Id,
                              ImageUrl = p.ImagemUrl,
                              Nome = p.Nome,
                          }).ToListAsync();
        }
    }
}
