using MassTransit;

namespace ProjetoRabbitMQ.Bus
{
    public interface IPublishBus
    {
        Task PublishAsync<T>(T Message, CancellationToken cancellationToken = default) where T : class;
    }
    internal class PublishBus : IPublishBus
    {
        private readonly IPublishEndpoint _busEndPoint;
        public PublishBus(IPublishEndpoint publish)
        {
            _busEndPoint = publish;
        }
        public Task PublishAsync<T>(T Message, CancellationToken cancellationToken) where T : class
        {
            return _busEndPoint.Publish(Message, cancellationToken);
        }
    }
}
