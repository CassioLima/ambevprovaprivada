using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Messages
{
    public class SaleCreated
    {
        public Guid SaleId { get; init; }
        public DateTime CreatedAt { get; init; }
        public string CustomerId { get; init; } = string.Empty;
        public decimal TotalAmount { get; init; }
    }
}