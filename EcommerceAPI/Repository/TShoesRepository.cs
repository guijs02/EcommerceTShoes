using EcommerceAPI.Repository.Interfaces;
using EcommerceTShoes.Model;
using GeekShopping.ProductAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EcommerceAPI.Repository
{
    public class TShoesRepository : ITShoesRepository
    {
        private readonly SQLServerContext _db;
        public TShoesRepository(SQLServerContext db)
        {
            _db = db;
        }
        public async Task<List<Produto>> GetProdutosByGenero(int idgenero)
        {
            return idgenero switch
            {
                1 => await _db.Products.Where(w => (int)w.Genero == idgenero).ToListAsync(),
                2 => await _db.Products.Where(w => (int)w.Genero == idgenero).ToListAsync(),
                _ => throw new Exception("Não há esse genero no banco de dados")            
            };
        }    
        public async Task<Produto> GetProduto(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<List<Produto>> GetAllProdutos()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
