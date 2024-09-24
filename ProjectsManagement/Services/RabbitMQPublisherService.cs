using RabbitMQ.Client;
using System.Text;

namespace ProjectsManagement.Services
{
    public class RabbitMQPublisherService
    {
        IConnection _connection;
        IModel _channel;

        public RabbitMQPublisherService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish("LoggingExchange", "key1", body: body);
        }

    }
}
