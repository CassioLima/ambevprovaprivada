using System;
using Xunit;
using Backend.Domain.Entities;

namespace Backend.Domain.Tests
{
    public class StockControlTests
    {
        [Fact(DisplayName = "StockControl should create with valid data")]
        public void StockControl_Should_Create()
        {
            var stock = new StockControl(Guid.NewGuid(), 10, "Entrada");
            Assert.NotEqual(Guid.Empty, stock.Id);
            Assert.Equal(10, stock.Quantity);
            Assert.Equal("Entrada", stock.MovementType);
        }

        [Fact(DisplayName = "StockControl should throw exception when quantity is invalid")]
        public void StockControl_Should_Throw_When_Quantity_Invalid()
        {
            Assert.Throws<ArgumentException>(() => new StockControl(Guid.NewGuid(), 0, "Entrada"));
        }
    }
}
