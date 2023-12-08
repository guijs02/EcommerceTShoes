using EcommerceOrderAPI.Domain.Messages;
using EcommerceOrderAPI.Domain.Model;

namespace EcommerceOrderAPI.Infraestructure.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(Order order);
        Task UpdateOrderPaymentStatus(PaymentMessage vo);
    }
}
