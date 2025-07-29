using AutoMapper;
using MediatR;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using MassTransit;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleCommandResult>
{
    private readonly DefaultContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISaleRepository _saleRepository;

    public CreateSaleCommandHandler(DefaultContext context, IMapper mapper, IPublishEndpoint publishEndpoint, ISaleRepository saleRepository)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _saleRepository = saleRepository;
    }

    public async Task<CreateSaleCommandResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);
        if (customer == null)
            throw new InvalidOperationException($"Customer {request.CustomerId} not found.");

        var sale = new Sale(request.SaleNumber, request.SaleDate, customer, request.Branch, request.PaymentMethod);

        foreach (var item in request.Items)
        {
            var product = await _context.Products.FindAsync(new object[] { item.ProductId }, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Produto {item.ProductId} not found.");

            if (item.Quantity > product.StockQuantity)
                throw new InvalidOperationException($"Insufficient stock for the product {product.Name}.");

            sale.AddItem(product.Id, product.Name, item.Quantity, product.Price, item.DiscountPercentage);

            product.DecreaseStock(item.Quantity);
        }

        await _saleRepository.CreateAsync(sale, cancellationToken);

        var saleEvent = new SaleCreatedEvent
        {
            SaleId = sale.Id,
            CreatedAt = sale.SaleDate,
            CustomerId = sale.CustomerId,
            TotalAmount = sale.TotalAmount
        };

        await _publishEndpoint.Publish<SaleCreatedEvent>(saleEvent);    

        return _mapper.Map<CreateSaleCommandResult>(sale);
    }
}
