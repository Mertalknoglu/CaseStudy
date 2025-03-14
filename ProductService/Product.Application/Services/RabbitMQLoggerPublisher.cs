using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Product.Application.Interfaces.Logging;
using Product.Domain.DTOs;

namespace Product.Application.Services
{
    public class RabbitMQLoggerPublisher : IRabbitMQLoggerPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<RabbitMQLoggerPublisher> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Kullanıcının endpoint bilgisi için

        public RabbitMQLoggerPublisher(IPublishEndpoint publishEndpoint,
                                       ILogger<RabbitMQLoggerPublisher> logger,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task PublishLogAsync(string level, string message, string endpoint)
        {
            var logMessage = new LogMessage
            {
                Level = level,
                Message = message,
                Timestamp = DateTime.UtcNow,
                Endpoint = endpoint
            };

            await _publishEndpoint.Publish(logMessage);
        }


    }
}
