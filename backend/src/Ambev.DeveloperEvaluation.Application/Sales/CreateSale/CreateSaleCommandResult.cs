using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandResult
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<SaleItemResultDto> Items { get; set; } = new();
}

public class SaleItemResultDto
{
    public Guid ProductId { get; set; }
    public string ProductDescription { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal Total { get; set; }
}
