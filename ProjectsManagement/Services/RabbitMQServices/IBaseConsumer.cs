using ProjectsManagement.ViewModels.RabbitMQMessages;

namespace ProjectsManagement.Services.RabbitMQServices
{
    public interface IBaseConsumer<T> where T : BaseMessage
    {
        Task Consume(T message);
    }
}
