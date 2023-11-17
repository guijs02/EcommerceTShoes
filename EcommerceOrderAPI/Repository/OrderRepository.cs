using EcommerceOrderAPI.Context;
using EcommerceOrderAPI.Model;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace EcommerceOrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public readonly DbContextOptions<SQLServerContext> _db;
        public OrderRepository(DbContextOptions<SQLServerContext> db)
        {
            _db = db;
        }
        public async Task<bool> AddOrder(Order order)
        {
            await using var context = new SQLServerContext(_db);
            context.Order.Add(order);
            context.SaveChanges();
            return true;
        }
    }
}
