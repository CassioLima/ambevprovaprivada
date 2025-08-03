using MassTransit;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCancelledConsumer : IConsumer<SaleCancelledEvent>
{
    private readonly ILogger<SaleCancelledConsumer> _logger;

    public SaleCancelledConsumer(ILogger<SaleCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SaleCancelledEvent> context)
    {
        _logger.LogInformation("SaleCancelled recebido: SaleId={SaleId}, Reason={Reason}",
            context.Message.SaleId, context.Message.Reason);

        return Task.CompletedTask;
    }
}
