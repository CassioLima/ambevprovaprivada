using MassTransit;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;

public class SaleModifiedConsumer : IConsumer<SaleModifiedEvent>
{
    private readonly ILogger<SaleModifiedConsumer> _logger;

    public SaleModifiedConsumer(ILogger<SaleModifiedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SaleModifiedEvent> context)
    {
        _logger.LogInformation("SaleModified recebido: SaleId={SaleId}, NewTotal={NewTotalAmount}",
            context.Message.SaleId, context.Message.NewTotalAmount);

        return Task.CompletedTask;
    }
}
