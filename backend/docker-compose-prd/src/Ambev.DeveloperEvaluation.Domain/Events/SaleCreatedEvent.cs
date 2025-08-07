using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; init; }
        public DateTime CreatedAt { get; init; }
        public Guid CustomerId { get; init; }
        public decimal TotalAmount { get; init; }
    }
}