using MessageBus;

namespace EcommercePaymentAPI.Appilcation.Messages
{
    public class UpdatePaymentResultMessage : BaseMessage
    {
        public int OrderId { get; set; }
        public bool Status { get; set; }
    }
}
