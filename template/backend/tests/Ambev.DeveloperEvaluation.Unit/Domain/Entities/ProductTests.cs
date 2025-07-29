using System;
using Xunit;
using Backend.Domain.Entities;

namespace Backend.Domain.Tests
{
    public class ProductTests
    {
        [Fact(DisplayName = "Product should be created with stock and price")]
        public void Product_Should_Be_Created()
        {
            var product = new Product("Product X", "Description", 100m, 10);
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal(10, product.StockQuantity);
        }

        [Fact(DisplayName = "Product stock increase should work")]
        public void Product_Should_IncreaseStock()
        {
            var product = new Product("Product X", "Description", 100m, 10);
            product.IncreaseStock(5);
            Assert.Equal(15, product.StockQuantity);
        }

        [Fact(DisplayName = "Product should decrease stock when valid")]
        public void Product_Should_DecreaseStock()
        {
            var product = new Product("Product X", "Description", 100m, 10);
            product.DecreaseStock(3);
            Assert.Equal(7, product.StockQuantity);
        }

        [Fact(DisplayName = "Product should throw when stock is insufficient")]
        public void Product_Should_Throw_When_Insufficient_Stock()
        {
            var product = new Product("Product X", "Description", 100m, 2);
            Assert.Throws<InvalidOperationException>(() => product.ValidateStock(5));
        }
    }
}
