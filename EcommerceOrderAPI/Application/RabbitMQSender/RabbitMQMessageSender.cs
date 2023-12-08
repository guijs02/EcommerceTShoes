using EcommerceOrderAPI.Domain.Messages;
using MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EcommerceOrderAPI.Application.RabbitMQSender
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

        public void SendMessage(BaseMessage message, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queueName, false, false, false, arguments: null);
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }

        }

        private byte[] GetMessageAsByteArray(object message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize((PaymentVO)message, options);
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
