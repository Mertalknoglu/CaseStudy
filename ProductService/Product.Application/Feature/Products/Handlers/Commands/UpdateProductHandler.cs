using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Feature.Products.Commands;
using Product.Application.Interfaces;
using Product.Application.Interfaces.Logging;
using Product.Domain.DTOs;

namespace Product.Application.Feature.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IRabbitMQLoggerPublisher _rabbitLogger;

        public UpdateProductCommandHandler(IProductRepository productRepository,
                                           ILogger<UpdateProductCommandHandler> logger,
                                           IRabbitMQLoggerPublisher rabbitLogger)
        {
            _productRepository = productRepository;
            _logger = logger;
            _rabbitLogger = rabbitLogger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    _logger.LogWarning($"Product not found: {request.Id}");
                    await _rabbitLogger.PublishLogAsync("Warning", $"Product not found: {request.Id}", "/api/product");
                    return new UpdateProductCommandResponse
                    {
                        IsSuccess = false,
                        Message = "Product not found"
                    };
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Stock = request.Stock;

                await _productRepository.UpdateAsync(product);
                _logger.LogInformation($"Product updated: {product.Name}");

                // ✅ RabbitMQ'ya Log Gönderme
                //await _rabbitLogger.PublishLogAsync("Info", $"Product updated: {product.Name}", "/api/product");

                return new UpdateProductCommandResponse
                {
                    IsSuccess = true,
                    Message = "Product successfully updated"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product: {ex.Message}");
                //await _rabbitLogger.PublishLogAsync("Error", $"Error updating product: {ex.Message}", "/api/product");
                throw;
            }
        }
    }
}
