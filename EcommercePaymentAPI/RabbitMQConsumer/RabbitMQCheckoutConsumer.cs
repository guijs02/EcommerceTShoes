using ECommerceTShoes.PaymentAPI.Messages;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ECommerceTShoes.PaymentAPI.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        //private IRabbitMQMessageSender _rabbitMQMessageSender;
        //private readonly IProcessPayment _processPayment;

        //public RabbitMQCheckoutConsumer(IProcessPayment processPayment,
        //                               IRabbitMQMessageSender rabbitMQMessageSender)
        //{
        //    var factory = new ConnectionFactory
        //    {
        //        HostName = "localhost",
        //        UserName = "guest",
        //        Password = "guest",
        //    };
        //    _connection = factory.CreateConnection();

        //    _channel = _connection.CreateModel();
        //    _channel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        //    _processPayment = processPayment;
        //    _rabbitMQMessageSender = rabbitMQMessageSender;
        //}

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
            _channel.BasicConsume("orderpaymentprocessqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {

            //var result = _processPayment.PaymentProcessor();
            //UpdatePaymentResultMessage paymentResult = new()
            //{
            //    Status = result,
            //    OrderId = vo.OrderId,
            //};

            try
            {
                //_rabbitMQMessageSender.SendMessage(paymentResult);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
