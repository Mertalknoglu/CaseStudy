using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Feature.Products.Commands;
using Product.Application.Interfaces;
using Product.Application.Interfaces.Logging;
using Product.Domain.DTOs;

namespace Product.Application.Feature.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IRabbitMQLoggerPublisher _rabbitLogger;

        public CreateProductCommandHandler(IProductRepository productRepository,
                                           ILogger<CreateProductCommandHandler> logger,
                                           IRabbitMQLoggerPublisher rabbitLogger)
        {
            _productRepository = productRepository;
            _logger = logger;
            _rabbitLogger = rabbitLogger;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product.Domain.Entities.Products
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock
                };

                await _productRepository.AddAsync(product);
                _logger.LogInformation($"Product added: {product.Name}");

                // ✅ RabbitMQ'ya Log Gönderme
                await _rabbitLogger.PublishLogAsync("Info", $"Product added: {product.Name}", "/api/product");

                return new CreateProductCommandResponse
                {
                    IsSuccess = true,
                    Message = "Product successfully created",
                    ProductId = product.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");

                // ✅ Hata durumunda RabbitMQ'ya log gönderme
                await _rabbitLogger.PublishLogAsync("Error", $"Error creating product: {ex.Message}", "/api/product");

                throw;
            }
        }
    }
}
