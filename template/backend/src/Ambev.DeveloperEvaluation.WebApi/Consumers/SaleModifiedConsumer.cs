using MassTransit;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.WebApi.Messages;

public class SaleModifiedConsumer : IConsumer<SaleModified>
{
    private readonly ILogger<SaleModifiedConsumer> _logger;

    public SaleModifiedConsumer(ILogger<SaleModifiedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SaleModified> context)
    {
        _logger.LogInformation("SaleModified recebido: SaleId={SaleId}, NewTotal={NewTotalAmount}",
            context.Message.SaleId, context.Message.NewTotalAmount);

        return Task.CompletedTask;
    }
}
