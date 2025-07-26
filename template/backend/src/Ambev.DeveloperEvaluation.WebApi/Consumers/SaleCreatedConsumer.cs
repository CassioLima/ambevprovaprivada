using MassTransit;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.WebApi.Messages;
public class SaleCreatedConsumer : IConsumer<SaleCreated>
{
    private readonly ILogger<SaleCreatedConsumer> _logger;

    public SaleCreatedConsumer(ILogger<SaleCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SaleCreated> context)
    {
        _logger.LogInformation("SaleCreated recebido: SaleId={SaleId}, Total={TotalAmount}",
            context.Message.SaleId, context.Message.TotalAmount);

        // Lógica de negócio (ex: enviar email, atualizar estoque, etc.)

        return Task.CompletedTask;
    }
}
