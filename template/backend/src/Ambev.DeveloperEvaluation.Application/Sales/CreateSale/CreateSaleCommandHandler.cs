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
        var sale = new Sale(request.SaleNumber, request.SaleDate, request.CustomerId, request.Branch, request.PaymentMethod);


        foreach (var item in request.Items)
        {
            // Busca o produto
            var product = await _context.Products.FindAsync(new object[] { item.ProductId }, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Produto {item.ProductId} não encontrado.");

            // Verifica estoque antes de adicionar item
            if (item.Quantity > product.StockQuantity)
                throw new InvalidOperationException($"Estoque insuficiente para o produto {product.Name}.");

            // Adiciona item com dados oficiais do produto
            sale.AddItem(product.Id, product.Name, item.Quantity, product.Price);

            // Atualiza estoque (gera movimentação automática)
            product.DecreaseStock(item.Quantity);
        }

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateSaleCommandResult>(sale);
    }
}
