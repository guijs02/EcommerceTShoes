using EcommerceCartAPI.Messages;
using MessageBus;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EcommerceCartAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _username = "guest";

            CreateConnection();


        }
        public void SendMessage(BaseMessage baseMessage, string queueName)
        {
            using var _channel = _connection.CreateModel();
            _channel.QueueDeclare(queueName, false, false, false);
            var body = GetMessageAsByte(baseMessage);
            _channel.BasicPublish(exchange:"", routingKey: queueName, basicProperties: null, body: body);   
        }
        private byte[] GetMessageAsByte(object message)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            var jsonContent = JsonSerializer.Serialize((CheckoutMessage)message, options);
            var body = Encoding.UTF8.GetBytes(jsonContent);
            return body;
        }
        void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    Password = _password,
                    UserName = _username,
                }; 

                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
