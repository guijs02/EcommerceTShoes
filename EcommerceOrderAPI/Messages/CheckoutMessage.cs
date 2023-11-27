using EcommerceCartAPI.Value_Object;
using EcommerceOrderAPI.Model;
using MessageBus;

namespace EcommerceOrderAPI.Message
{
    public class CheckoutMessage : BaseMessage
    {
        public PaymentVO Payment { get; set; }
        public List<OrderDetail> Cart { get; set; }
        public OrderSummary OrderSummary { get; set; }
    }
    public class OrderSummary
    {
        public string Frete { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
