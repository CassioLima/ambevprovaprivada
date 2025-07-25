using MediatR;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand>
{
    private readonly DefaultContext _context;

    public DeleteSaleCommandHandler(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException("Sale not found");

        sale.Cancel();
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    Task IRequestHandler<DeleteSaleCommand>.Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
