using MessageBus;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using EcommercePaymentAPI.Appilcation.Messages;

namespace EcommercePaymentAPI.Appilcation.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private IRabbitMQMessageSender _messageSender;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
            //_connection = "guest";
        }

        public void SendMessage(BaseMessage message, string queue)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: queue, false, false, false);
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);
            }

        }

        private byte[] GetMessageAsByteArray(object message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize((UpdatePaymentResultMessage)message, options);
            var body = Encoding.UTF8.GetBytes(json);

            return body;
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password,
                };

                _connection = factory.CreateConnection();

            }
            catch (Exception e)
            {

                throw;
            }
        }
        private bool ConnectionExists()
        {
            if (_connection is not null)
                return true;

            CreateConnection();

            return _connection is not null;

        }
    }
}
