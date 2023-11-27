using EcommerceOrderAPI.Message;
using EcommerceOrderAPI.Model;
using EcommerceOrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EcommerceOrderAPI.RabbitMQConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private string _hostName;
        private string _password;
        private string _username;
        private IConnection _connection;
        private IModel _channel;
        private readonly IOrderRepository _orderRepository;
        public RabbitMQCheckoutConsumer(OrderRepository orderRepository)
        {
            _hostName = "localhost";
            _password = "guest";
            _username = "guest";

            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                Password = _password,
                UserName = _username,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare("checkoutqueue", false, false, false);
            _orderRepository = orderRepository;
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
                    Id = item.Id,
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

            try
            {
                await _orderRepository.AddOrder(order);
                //_rabbitMQMessageSender.SendMessage(order, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
