using MassTransit;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
public class SaleCreatedConsumer : IConsumer<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedConsumer> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;

    public SaleCreatedConsumer(ILogger<SaleCreatedConsumer> logger, IProductRepository productRepository, ISaleRepository saleRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _saleRepository = saleRepository;
    }

    public Task Consume(ConsumeContext<SaleCreatedEvent> context)
    {
        _logger.LogInformation("SaleCreated recebido: SaleId={SaleId}, Total={TotalAmount}",
            context.Message.SaleId, context.Message.TotalAmount);

        _saleRepository.GetByIdAsync(context.Message.SaleId)
        .ContinueWith(saleTask =>
        {
            if (saleTask.Result == null)
            {
                _logger.LogWarning("Sale not found for SaleId={SaleId}", context.Message.SaleId);
                return;
            }

            // Process the sale, e.g., update stock, notify other services, etc.
            foreach (var item in saleTask.Result.Items)
            {
                var product = _productRepository.GetByIdAsync(item.ProductId).Result;
                if (product != null)
                {
                    product.DecreaseStock(item.Quantity);
                    _productRepository.UpdateAsync(product);
                }
            }
        });
        

        return Task.CompletedTask;
    }
}
