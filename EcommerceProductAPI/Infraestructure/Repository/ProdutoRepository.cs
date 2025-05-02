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
        public async Task<List<ProdutoDto>> GetProdutosByGeneroAsync(int idgenero)
        {
            return await GetListByGenero(idgenero);
        }
        public async Task<Produto> GetProdutoAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<List<ProdutoDto>> GetAllPrdutosAsync()
        {
            var query = from p in _db.Products
                         select new ProdutoDto
                         {
                             Id = p.Id,
                             ImageUrl = p.ImagemUrl,
                             Nome = p.Nome,
                             Preco = p.Preco

                         };

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
