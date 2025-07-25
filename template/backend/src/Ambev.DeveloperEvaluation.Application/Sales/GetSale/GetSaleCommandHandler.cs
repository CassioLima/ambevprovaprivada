using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommandHandler : IRequestHandler<GetSaleCommand, GetSaleCommandResult>
{
    private readonly DefaultContext _context;
    private readonly IMapper _mapper;

    public GetSaleCommandHandler(DefaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetSaleCommandResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException("Sale not found");

        return _mapper.Map<GetSaleCommandResult>(sale);
    }
}
