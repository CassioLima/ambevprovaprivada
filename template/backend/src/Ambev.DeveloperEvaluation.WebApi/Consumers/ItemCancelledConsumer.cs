using MassTransit;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.WebApi.Messages;
public class ItemCancelledConsumer : IConsumer<ItemCancelled>
{
    private readonly ILogger<ItemCancelledConsumer> _logger;

    public ItemCancelledConsumer(ILogger<ItemCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ItemCancelled> context)
    {
        _logger.LogInformation("ItemCancelled recebido: SaleId={SaleId}, ItemId={ItemId}, Reason={Reason}",
            context.Message.SaleId, context.Message.ItemId, context.Message.Reason);

        return Task.CompletedTask;
    }
}
