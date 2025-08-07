using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public record DeleteSaleCommand(Guid ItemId) : IRequest<DeleteSaleCommandResult>;
