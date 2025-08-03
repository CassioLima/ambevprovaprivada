using System;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Tests.Events
{
    public class SaleCancelledEventTests
    {
        [Fact(DisplayName = "SaleCancelledEvent should be created correctly")]
        public void SaleCancelledEvent_Should_Be_Created()
        {
            var saleId = Guid.NewGuid();
            var cancelledAt = DateTime.UtcNow;
            var reason = "Customer request";

            var evt = new SaleCancelledEvent
            {
                SaleId = saleId,
                CancelledAt = cancelledAt,
                Reason = reason
            };

            Assert.Equal(saleId, evt.SaleId);
            Assert.Equal(cancelledAt, evt.CancelledAt);
            Assert.Equal(reason, evt.Reason);
        }
    }
}
