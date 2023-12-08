using EcommerceOrderAPI.Domain.Model;
using MessageBus;

namespace EcommerceOrderAPI.Domain.Messages
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
