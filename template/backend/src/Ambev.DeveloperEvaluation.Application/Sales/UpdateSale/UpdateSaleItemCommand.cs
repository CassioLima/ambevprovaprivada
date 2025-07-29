using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class UpdateSaleItemCommand : IRequest
{
    public Guid Id { get; private set; }
    public Guid ItemId { get; private set; }

    public UpdateSaleItemCommand(Guid id)
    {
        Id = id;
    }

    public UpdateSaleItemCommand(Guid id, Guid itemId)
    {
        Id = id;
        ItemId = itemId;    
    }
}
