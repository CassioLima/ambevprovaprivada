using System;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Tests.Events
{
    public class SaleModifiedEventTests
    {
        [Fact(DisplayName = "SaleModifiedEvent should be created correctly")]
        public void SaleModifiedEvent_Should_Be_Created()
        {
            var saleId = Guid.NewGuid();
            var modifiedAt = DateTime.UtcNow;
            var newTotal = 200.50m;
            var modifiedBy = "admin";

            var evt = new SaleModifiedEvent
            {
                SaleId = saleId,
                ModifiedAt = modifiedAt,
                NewTotalAmount = newTotal,
                ModifiedBy = modifiedBy
            };

            Assert.Equal(saleId, evt.SaleId);
            Assert.Equal(modifiedAt, evt.ModifiedAt);
            Assert.Equal(newTotal, evt.NewTotalAmount);
            Assert.Equal(modifiedBy, evt.ModifiedBy);
        }
    }
}
