using MassTransit;
using LogService.Models;

namespace LogService.Consumers
{
    public class LogMessageConsumer : IConsumer<LogMessage>
    {
        private readonly ILogger<LogMessageConsumer> _logger;

        public LogMessageConsumer(ILogger<LogMessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<LogMessage> context)
        {
            var log = context.Message;

            // Log'u konsola yazdır
            _logger.LogInformation($"📥 Log Received - Level: {log.Level}, Message: {log.Message}, Endpoint: {log.Endpoint}, Timestamp: {log.Timestamp}");

            await Task.CompletedTask;
        }
    }
}
