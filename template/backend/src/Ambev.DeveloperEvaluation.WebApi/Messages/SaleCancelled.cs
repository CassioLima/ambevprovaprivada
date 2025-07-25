using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Messages
{
    public class SaleCancelled
    {
        public Guid SaleId { get; init; }
        public DateTime CancelledAt { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
}