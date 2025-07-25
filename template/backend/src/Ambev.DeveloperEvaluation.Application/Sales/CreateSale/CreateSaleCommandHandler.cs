using AutoMapper;
using MediatR;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleCommandResult>
{
    private readonly DefaultContext _context;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(DefaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CreateSaleCommandResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale(request.SaleNumber, request.SaleDate, request.Customer, request.Branch);

        foreach (var item in request.Items)
        {
            sale.AddItem(item.ProductId, item.ProductDescription, item.Quantity, item.UnitPrice);
        }

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateSaleCommandResult>(sale);
    }
}
