using EcommerceOrderAPI.Model;
using Humanizer;

namespace EcommerceOrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(Order order);
    }
}
