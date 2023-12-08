namespace EcommerceOrderAPI.Domain.Messages
{
    public class PaymentMessage
    {
        public int OrderId { get; set; }
        public bool Status { get; set; }
    }
}
