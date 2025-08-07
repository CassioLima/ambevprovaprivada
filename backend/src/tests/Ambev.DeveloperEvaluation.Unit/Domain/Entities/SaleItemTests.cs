using System;
using Xunit;
using Backend.Domain.Entities;

namespace Backend.Domain.Tests
{
    public class SaleItemTests
    {
        [Fact(DisplayName = "SaleItem should apply correct discount")]
        public void SaleItem_Should_Apply_Discount()
        {
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 5, 10m, 0);
            Assert.Equal(10, item.DiscountPercentage); // quantidade entre 4 e 10 aplica 10%
        }

        [Fact(DisplayName = "SaleItem should update quantity and apply new discount")]
        public void SaleItem_Should_Update_Quantity_And_Discount()
        {
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 2, 10m, 0);
            item.UpdateQuantity(12);
            Assert.Equal(20, item.DiscountPercentage); // quantidade entre 10 e 20 aplica 20%
        }

        [Fact(DisplayName = "SaleItem should throw exception when quantity is invalid")]
        public void SaleItem_Should_Throw_When_Invalid_Quantity()
        {
            Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 0, 10m, 0));
        }
    }
}
