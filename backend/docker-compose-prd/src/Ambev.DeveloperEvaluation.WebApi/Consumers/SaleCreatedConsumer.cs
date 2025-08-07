using MassTransit;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
public class SaleCreatedConsumer : IConsumer<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SaleCreatedConsumer(ILogger<SaleCreatedConsumer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Consume(ConsumeContext<SaleCreatedEvent> context)
    {
        _logger.LogInformation("SaleCreated recebido: SaleId={SaleId}, Total={TotalAmount}",
            context.Message.SaleId, context.Message.TotalAmount);

        using var scope = _serviceProvider.CreateScope();
        var saleRepository = scope.ServiceProvider.GetRequiredService<ISaleRepository>();
        var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

        var sale = await saleRepository.GetByIdAsync(context.Message.SaleId, context.CancellationToken);
        if (sale == null)
        {
            _logger.LogWarning("Sale not found for SaleId={SaleId}", context.Message.SaleId);
            return;
        }

        foreach (var item in sale.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId, context.CancellationToken);
            if (product != null)
            {
                product.DecreaseStock(item.Quantity);
                await productRepository.UpdateAsync(product, context.CancellationToken);
            }
        }
    }
}
