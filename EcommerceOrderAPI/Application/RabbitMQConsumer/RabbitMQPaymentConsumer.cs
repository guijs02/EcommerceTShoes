using EcommerceOrderAPI.Domain.Messages;
using EcommerceOrderAPI.Infraestructure.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace EcommerceOrderAPI.Application.RabbitMQConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private OrderRepository _orderRepository;

        public RabbitMQPaymentConsumer(OrderRepository orderRepository)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmqTShoes",
                UserName = "guest",
                Password = "guest",
            };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "queueresultpayment", false, false, false, arguments: null);
            _orderRepository = orderRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content);
                ProcessPayment(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("queueresultpayment", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {

            try
            {
                await _orderRepository.UpdateOrderPaymentStatus(vo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
