using MassTransit;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.WebApi.Messages;

public class SaleCancelledConsumer : IConsumer<SaleCancelled>
{
    private readonly ILogger<SaleCancelledConsumer> _logger;

    public SaleCancelledConsumer(ILogger<SaleCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SaleCancelled> context)
    {
        _logger.LogInformation("SaleCancelled recebido: SaleId={SaleId}, Reason={Reason}",
            context.Message.SaleId, context.Message.Reason);

        return Task.CompletedTask;
    }
}
