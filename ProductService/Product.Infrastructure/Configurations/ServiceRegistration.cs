using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Interfaces;
using Product.Application.Interfaces.Logging;
using Product.Application.Services;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repositories;
using StackExchange.Redis;

namespace Product.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // **SQL Server Bağlantısı**
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure());
            });

            // **Repository Bağımlılıklarını Kaydetme**
            services.AddScoped<IProductRepository, ProductRepository>();

            // **Redis Cache Bağlantısı**
            services.AddSingleton<IConnectionMultiplexer>(provider =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "ProductCache_";
            });



            services.AddMassTransitHostedService(true);

            // **RabbitMQ Logger'ı Dependency Injection'a ekleme**
            services.AddScoped<IRabbitMQLoggerPublisher, RabbitMQLoggerPublisher>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
