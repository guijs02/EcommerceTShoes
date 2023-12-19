using EcommerceCartAPI.Domain.Models;
using EcommerceCartAPI.Value_Object;
using MessageBus;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCartAPI.Domain.Messages
{
    public class CheckoutMessage : BaseMessage
    {
        [Required]
        public PaymentVO Payment { get; set; }
        [Required]
        public List<CarrinhoDeCompra> Cart { get; set; }
        
        public OrderSummary OrderSummary { get; set; }
    }
    public class OrderSummary
    {
        [Required]
        public string Frete { get; set; }
        public decimal Desconto { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
    }
}
