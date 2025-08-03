using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "Credit Card";
        public List<SaleItemRequest> Items { get; set; } = new();
    }

    public class SaleItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPercentage { get; set; } = 0;
    }
}
