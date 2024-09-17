using MassTransit;
using ProjetoRabbitMQ.Bus;

namespace ProjetoRabbitMQ.Extension
{
    internal static class AppExtensions
    {
        public static void AddRabbitMQServices(this IServiceCollection services)
        {
            //guest
            services.AddMassTransit(busConfigurator =>
            {
                services.AddTransient<IPublishBus, PublishBus>();
                busConfigurator.AddConsumer<RelatorioSolicitadoEventConsumer>();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri("amqp://localhost:5672"), host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });//colocar no appSettings
                    configurator.ConfigureEndpoints(context);
                });
            });
        }
    }
}
