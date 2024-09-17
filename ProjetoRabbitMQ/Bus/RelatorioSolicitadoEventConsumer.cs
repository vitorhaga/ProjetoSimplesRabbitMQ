using MassTransit;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.Bus
{
    internal sealed class RelatorioSolicitadoEventConsumer : IConsumer<RelatorioSolicitadoEvent>
    {
        private readonly ILogger<RelatorioSolicitadoEventConsumer> _logger;
        public RelatorioSolicitadoEventConsumer(ILogger<RelatorioSolicitadoEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
        {
            //sempre usar logs e correlationId
            var message = context.Message;
            _logger.LogInformation("Processando relatório Id:{Id}, Nome:{Nome}", message.Id, message.Name);
            //delay pra fingir um processamento
            await Task.Delay(10000);
            //atualizar relatorio
            var relatorio = Lista.Relatorios.FirstOrDefault(x => x.Id == message.Id);
            if(relatorio != null)
            {
                relatorio.Status = "Completo";
                relatorio.ProcessedTime = DateTime.Now;
            }
            _logger.LogInformation("Relatório Processado Id:{Id}, Nome:{Nome}", message.Id, message.Name);

        }
    }
}
