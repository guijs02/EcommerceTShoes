using EcommerceOrderAPI.Application.RabbitMQSender;
using EcommerceOrderAPI.Domain.Messages;
using EcommerceOrderAPI.Domain.Model;
using EcommerceOrderAPI.Infraestructure.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EcommerceOrderAPI.Application.RabbitMQConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private string _hostname;
        private string _password;
        private string _username;
        private IConnection _connection;
        private IModel _channel;
        private readonly IOrderRepository _orderRepository;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(OrderRepository orderRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _hostname = "rabbitmqTShoes";
            _password = "guest";
            _username = "guest";

            if (ConnectionExists())
            {
                _channel = _connection.CreateModel();

                _rabbitMQMessageSender = rabbitMQMessageSender;
                _orderRepository = orderRepository;

                _channel.QueueDeclare("checkoutqueue", false, false, false);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var obj = JsonSerializer.Deserialize<CheckoutMessage>(content);
                await ProcessOrderAsync(obj);
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("checkoutqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrderAsync(CheckoutMessage checkoutMessage)
        {
            Order order = new()
            {
                Cvv = checkoutMessage.Payment.Cvv,
                NomeTitular = checkoutMessage.Payment.Nome,
                Numero = checkoutMessage.Payment.Numero,
                DateTime = checkoutMessage.MessageCreated,
                Validade = checkoutMessage.Payment.Validade,
                OrderDetail = new()
            };
            foreach (var item in checkoutMessage.Cart)
            {
                var detail = new OrderDetail()
                {
                    //Id = 0,
                    Descricao = item.Descricao,
                    ProdutoId = item.ProdutoId,
                    ImagemUrl = item.ImagemUrl,
                    Nome = item.Nome,
                    Preco = item.Preco,
                    Quantidade = item.Quantidade,
                    ValorTotal = checkoutMessage.OrderSummary.ValorTotal,
                    Tamanho = item.Tamanho,
                    UserId = item.UserId,
                    OrderId = item.OrderId,
                };
                order.OrderDetail.Add(detail);
            }

            await _orderRepository.AddOrder(order);

            PaymentVO paymentVO = new()
            {
                Nome = order.NomeTitular,
                Cvv = order.Cvv,
                Numero = order.NomeTitular,
                Validade = order.NomeTitular,
                OrderId = order.OrderDetail.First().OrderId
            };

            try
            {
                _rabbitMQMessageSender.SendMessage(paymentVO, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
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
