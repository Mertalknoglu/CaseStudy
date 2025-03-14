using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Feature.Products.Commands;
using Product.Application.Interfaces;
using Product.Application.Interfaces.Logging;
using Product.Domain.DTOs;

namespace Product.Application.Feature.Products.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IRabbitMQLoggerPublisher _rabbitLogger;

        public DeleteProductCommandHandler(IProductRepository productRepository,
                                           ILogger<DeleteProductCommandHandler> logger,
                                           IRabbitMQLoggerPublisher rabbitLogger)
        {
            _productRepository = productRepository;
            _logger = logger;
            _rabbitLogger = rabbitLogger;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    _logger.LogWarning($"Product not found: {request.Id}");
                    await _rabbitLogger.PublishLogAsync("Warning", $"Product not found: {request.Id}", "/api/product");
                    return new DeleteProductCommandResponse
                    {
                        IsSuccess = false,
                        Message = "Product not found"
                    };
                }

                await _productRepository.DeleteAsync(product);
                _logger.LogInformation($"Product deleted: {product.Name}");

                // ✅ RabbitMQ'ya Log Gönderme
                await _rabbitLogger.PublishLogAsync("Info", $"Product deleted: {product.Name}", "/api/product");

                return new DeleteProductCommandResponse
                {
                    IsSuccess = true,
                    Message = "Product successfully deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product: {ex.Message}");
                await _rabbitLogger.PublishLogAsync("Error", $"Error deleting product: {ex.Message}", "/api/product");
                throw;
            }
        }
    }
}
