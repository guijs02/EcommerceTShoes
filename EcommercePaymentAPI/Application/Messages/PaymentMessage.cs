using MessageBus;

namespace EcommercePaymentAPI.Appilcation.Messages
{
    public class PaymentMessage : BaseMessage
    {
        public string Nome { get; set; }
        public string Cvv { get; set; }
        public string Numero { get; set; }
        public string Validade { get; set; }
        public int OrderId { get; set; }
    }
}
