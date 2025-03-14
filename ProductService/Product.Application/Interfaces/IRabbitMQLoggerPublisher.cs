namespace Product.Application.Interfaces.Logging
{
    public interface IRabbitMQLoggerPublisher
    {
        Task PublishLogAsync(string level, string message, string endpoint);

    }
}
