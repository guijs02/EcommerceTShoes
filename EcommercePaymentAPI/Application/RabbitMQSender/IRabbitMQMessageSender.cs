using MessageBus;

namespace EcommercePaymentAPI.Appilcation.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        public void SendMessage(BaseMessage message, string queue);
    }
}
