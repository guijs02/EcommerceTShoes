using MessageBus;

namespace EcommerceOrderAPI.Domain.Messages
{
    public class PaymentVO : BaseMessage
    {
        public string Nome { get; set; }
        public string Cvv { get; set; }
        public string Numero { get; set; }
        public string Validade { get; set; }
        public int OrderId { get; set; }
    }
}
