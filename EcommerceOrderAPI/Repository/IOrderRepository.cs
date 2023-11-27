using EcommerceOrderAPI.Model;

namespace EcommerceOrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(Order order);
    }
}
