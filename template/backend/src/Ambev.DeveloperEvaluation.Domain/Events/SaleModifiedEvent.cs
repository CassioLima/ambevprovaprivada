using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent
    {
        public Guid SaleId { get; init; }
        public DateTime ModifiedAt { get; init; }
        public decimal NewTotalAmount { get; init; }
        public string ModifiedBy { get; init; } = string.Empty;
    }
}