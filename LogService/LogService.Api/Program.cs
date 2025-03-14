using MassTransit;
using LogService.Consumers;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<LogMessageConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("log-service-queue", e =>
            {
                e.ConfigureConsumer<LogMessageConsumer>(context);
            });
        });
    });

    services.AddMassTransitHostedService(true);
});

var app = builder.Build();
await app.RunAsync();
