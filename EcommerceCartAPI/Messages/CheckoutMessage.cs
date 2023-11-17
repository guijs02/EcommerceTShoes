using EcommerceCartAPI.Models;
using EcommerceCartAPI.Value_Object;
using MessageBus;

namespace EcommerceCartAPI.Messages
{
    public class CheckoutMessage : BaseMessage
    {
        public PaymentVO Payment { get; set; }
        public List<CarrinhoDeCompra> Cart {  get; set; }
    }
}
