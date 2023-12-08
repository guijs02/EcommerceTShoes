using EcommerceCartAPI.Domain.Models;
using EcommerceCartAPI.Value_Object;
using MessageBus;

namespace EcommerceCartAPI.Domain.Messages
{
    public class CheckoutMessage : BaseMessage
    {
        public PaymentVO Payment { get; set; }
        public List<CarrinhoDeCompra> Cart { get; set; }

        public OrderSummary OrderSummary { get; set; }
    }
    public class OrderSummary
    {
        public string Frete { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
