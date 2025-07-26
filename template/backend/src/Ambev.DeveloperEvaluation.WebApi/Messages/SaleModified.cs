using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Messages
{
    public class SaleModified
    {
        public Guid SaleId { get; init; }
        public DateTime ModifiedAt { get; init; }
        public decimal NewTotalAmount { get; init; }
        public string ModifiedBy { get; init; } = string.Empty;
    }
}