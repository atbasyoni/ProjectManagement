using MediatR;
using ProjectsManagement.Data;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using ProjectsManagement.ViewModels.RabbitMQMessages;

namespace ProjectsManagement.Services
{
    public class RabbitMQConsumerService : IHostedService
    {
        IConnection _connection;
        IModel _channel;
        IMediator _mediator;
        ILogger _logger;

        public RabbitMQConsumerService(IMediator mediator, ILogger logger)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _mediator = mediator;

            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += Consumer_Received;

            _channel.BasicConsume("LoggingQueue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                var baseMessage = GetBaseMessage(message);

                await InvokeConsumer(baseMessage);

                _channel.BasicAck(e.DeliveryTag, multiple: false);
            }
            catch (BusinessException ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                _channel.BasicReject(e.DeliveryTag, requeue: false);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message.ToString());
                _channel.BasicReject(e.DeliveryTag, requeue: true);
            }
        }

        private async Task InvokeConsumer(BaseMessage baseMessage)
        {
            var nameSpace = "ProjectsManagement.Services.RabbitMQServices";

            var consumerType = Type.GetType($"{nameSpace}.{baseMessage.Type}Consumer, ProjectsManagement");

            if (consumerType is null)
                throw new Exception();

            var consumer = Activator.CreateInstance(consumerType, _mediator);
            var method = consumerType.GetMethod("Consume");

            method.Invoke(consumer, new object[] { baseMessage });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private BaseMessage GetBaseMessage(string message)
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(message);

            var typeName = jsonObject["Type"].ToString();

            var nameSpace = "ProjectsManagement.ViewModels.RabbitMQMessages";

            Type type = Type.GetType($"{nameSpace}.{typeName}, ProjectsManagement");

            if (type is null)
                throw new Exception();

            var baseMessage = Newtonsoft.Json.JsonConvert.DeserializeObject(message, type) as BaseMessage;

            baseMessage.Type = typeName.Replace("Message", "");

            return baseMessage;
        }
    }
}
