using EcommerceCartAPI.Value_Object;
using EcommerceOrderAPI.Model;
using MessageBus;

namespace EcommerceOrderAPI.Message
{
    public class CheckoutMessage : BaseMessage
    {
        public PaymentVO Payment { get; set; }
        public List<OrderDetail> Cart {  get; set; }
    }
}
