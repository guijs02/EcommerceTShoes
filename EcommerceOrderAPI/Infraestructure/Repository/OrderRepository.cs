using EcommerceOrderAPI.Domain.Messages;
using EcommerceOrderAPI.Domain.Model;
using EcommerceOrderAPI.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceOrderAPI.Infraestructure.Repository
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
        public async Task UpdateOrderPaymentStatus(PaymentMessage vo)
        {
            await using var context = new SQLServerContext(_db);
            var header = await context.Order.FirstOrDefaultAsync(o => o.Id == vo.OrderId);
            if (header is not null)
            {
                header.PaymentStatus = vo.Status;
                context.SaveChanges();
            }
        }
    }
}
