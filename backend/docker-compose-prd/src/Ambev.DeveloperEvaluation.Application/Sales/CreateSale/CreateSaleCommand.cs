using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleCommandResult>
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public List<SaleItemDto> Items { get; set; } = new();
}

public class SaleItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal DiscountPercentage { get; set; }
}

public class CreateSaleItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal DiscountPercentage { get; set; }
}

public class SaleItemDeletedEvent
{
    public Guid ItemId { get; set; }
    public DateTime DeletedAt { get; set; }
}
