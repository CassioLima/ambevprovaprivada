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
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;

    public CreateSaleCommandHandler(IMapper mapper, IPublishEndpoint publishEndpoint, ISaleRepository saleRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public async Task<CreateSaleCommandResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId , cancellationToken);
        if (customer == null)
            throw new InvalidOperationException($"Customer {request.CustomerId} not found.");

        var sale = new Sale(request.SaleNumber, request.SaleDate, customer, request.Branch, request.PaymentMethod);

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync( item.ProductId , cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Produto {item.ProductId} not found.");

            product.ValidateStock(item.Quantity);

            sale.AddItem(product.Id, product.Name, item.Quantity, product.Price, item.DiscountPercentage);

        }

        await _saleRepository.CreateAsync(sale, cancellationToken);

        await _publishEndpoint.Publish<SaleCreatedEvent>(new SaleCreatedEvent
        {
            SaleId = sale.Id,
            CreatedAt = sale.SaleDate,
            CustomerId = sale.CustomerId,
            TotalAmount = sale.TotalAmount
        });

        return _mapper.Map<CreateSaleCommandResult>(sale);
    }
}
