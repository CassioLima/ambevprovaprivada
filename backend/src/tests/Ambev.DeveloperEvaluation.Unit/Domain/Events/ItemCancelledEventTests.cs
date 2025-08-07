using System;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Tests.Events
{
    public class ItemCancelledEventTests
    {
        [Fact(DisplayName = "ItemCancelledEvent should be created correctly")]
        public void ItemCancelledEvent_Should_Be_Created()
        {
            var saleId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var cancelledAt = DateTime.UtcNow;
            var reason = "Product unavailable";

            var evt = new ItemCancelledEvent
            {
                SaleId = saleId,
                ItemId = itemId,
                CancelledAt = cancelledAt,
                Reason = reason
            };

            Assert.Equal(saleId, evt.SaleId);
            Assert.Equal(itemId, evt.ItemId);
            Assert.Equal(cancelledAt, evt.CancelledAt);
            Assert.Equal(reason, evt.Reason);
        }
    }
}
