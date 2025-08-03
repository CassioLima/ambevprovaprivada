using System;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Tests.Events
{
    public class SaleCreatedEventTests
    {
        [Fact(DisplayName = "SaleCreatedEvent should be created correctly")]
        public void SaleCreatedEvent_Should_Be_Created()
        {
            var saleId = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;
            var customerId = Guid.NewGuid();
            var totalAmount = 150.75m;

            var evt = new SaleCreatedEvent
            {
                SaleId = saleId,
                CreatedAt = createdAt,
                CustomerId = customerId,
                TotalAmount = totalAmount
            };

            Assert.Equal(saleId, evt.SaleId);
            Assert.Equal(createdAt, evt.CreatedAt);
            Assert.Equal(customerId, evt.CustomerId);
            Assert.Equal(totalAmount, evt.TotalAmount);
        }
    }
}
