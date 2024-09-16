using Microsoft.AspNetCore.Builder;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.EndPoints
{
    internal static class ApiEndpoints
    {
        public static void AddApiEndpoints(this WebApplication app)
        {
            app.MapPost("Solicitar-relatorio/{name}", (string name) =>
            {
                var solicitacao = new SolicitacaoRelatorio()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Status = "Pendente",
                    ProcessedTime = null
                };
                Lista.Relatorios!.Add(solicitacao);

                return Results.Ok(solicitacao);
            });

            app.MapGet("Relatorios", () => Lista.Relatorios);
        }
    }
}
