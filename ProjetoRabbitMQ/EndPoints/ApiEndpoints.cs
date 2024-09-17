using ProjetoRabbitMQ.Bus;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.EndPoints
{
    internal static class ApiEndpoints
    {
        public static void AddApiEndpoints(this WebApplication app)
        {
            app.MapPost("Solicitar-relatorio/{name}", async(string name, IPublishBus bus, CancellationToken cancellationToken = default) =>
            {
                var solicitacao = new SolicitacaoRelatorio()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Status = "Pendente",
                    ProcessedTime = null
                };
                Lista.Relatorios!.Add(solicitacao);

                var eventRequest = new RelatorioSolicitadoEvent(solicitacao.Id, solicitacao.Name);

                await bus.PublishAsync(eventRequest, cancellationToken);

                return Results.Ok(solicitacao);
            });

            app.MapGet("Relatorios", () => Lista.Relatorios);
        }
    }
}
