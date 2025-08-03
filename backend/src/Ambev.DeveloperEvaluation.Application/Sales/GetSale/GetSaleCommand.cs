using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommand : IRequest<GetSaleCommandResult>
{
    public Guid Id { get; set; }

    public GetSaleCommand(Guid id)
    {
        Id = id;
    }
}
