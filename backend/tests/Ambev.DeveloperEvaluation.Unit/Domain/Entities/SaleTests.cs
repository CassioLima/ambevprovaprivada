using System;
using Xunit;
using Backend.Domain.Entities;

namespace Backend.Domain.Tests
{
    public class SaleTests
    {
        [Fact(DisplayName = "Sale should add item and calculate total")]
        public void Sale_Should_AddItem_And_CalculateTotal()
        {
            var customer = new Customer("John Doe", "john@example.com", "(11)90000-0001", "Address");
            var sale = new Sale("S001", DateTime.UtcNow, customer, "Main Branch", "Cash");

            sale.AddItem(Guid.NewGuid(), "Product A", 2, 50m, 0);
            Assert.Single(sale.Items);
            Assert.Equal(100m, sale.TotalAmount);
        }

        [Fact(DisplayName = "Sale should throw when adding more than 20 items")]
        public void Sale_Should_Throw_When_Adding_More_Than_20_Items()
        {
            var customer = new Customer("John Doe", "john@example.com", "(11)90000-0001", "Address");
            var sale = new Sale("S001", DateTime.UtcNow, customer, "Main Branch", "Cash");

            Assert.Throws<InvalidOperationException>(() =>
                sale.AddItem(Guid.NewGuid(), "Product A", 21, 50m, 0));
        }

        [Fact(DisplayName = "Sale should complete successfully")]
        public void Sale_Should_Complete()
        {
            var customer = new Customer("John Doe", "john@example.com", "(11)90000-0001", "Address");
            var sale = new Sale("S001", DateTime.UtcNow, customer, "Main Branch", "Cash");
            sale.AddItem(Guid.NewGuid(), "Product A", 1, 50m, 0);

            sale.Complete();
            Assert.Equal("Paid", sale.Status);
        }

        [Fact(DisplayName = "Sale should cancel successfully")]
        public void Sale_Should_Cancel()
        {
            var customer = new Customer("John Doe", "john@example.com", "(11)90000-0001", "Address");
            var sale = new Sale("S001", DateTime.UtcNow, customer, "Main Branch", "Cash");

            sale.Cancel();
            Assert.Equal("Cancelled", sale.Status);
        }
    }
}
