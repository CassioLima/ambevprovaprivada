using MediatR;
using MassTransit;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleCommandResult>
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteSaleCommandHandler(
        ISaleItemRepository saleItemRepository,
        IPublishEndpoint publishEndpoint)
    {
        _saleItemRepository = saleItemRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<DeleteSaleCommandResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var saleItem = await _saleItemRepository.GetByIdAsync(request.ItemId, cancellationToken);

        if (saleItem == null)
        {
            return new DeleteSaleCommandResult
            {
                Success = false,
                Message = $"Item {request.ItemId} not found."
            };
        }

        var deleted = await _saleItemRepository.DeleteAsync(request.ItemId, cancellationToken);

        if (!deleted)
        {
            return new DeleteSaleCommandResult
            {
                Success = false,
                Message = "Failed to delete item."
            };
        }

        await _publishEndpoint.Publish(new SaleItemDeletedEvent
        {
            ItemId = request.ItemId,
            DeletedAt = DateTime.UtcNow
        }, cancellationToken);

        return new DeleteSaleCommandResult
        {
            Success = true,
            Message = "Item deleted successfully."
        };
    }
}

