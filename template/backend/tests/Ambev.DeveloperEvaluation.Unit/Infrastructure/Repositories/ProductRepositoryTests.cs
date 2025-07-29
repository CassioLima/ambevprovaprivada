using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private DefaultContext GetContext() =>
            new DefaultContext(new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        [Fact(DisplayName = "Should create and retrieve product")]
        public async Task Should_Create_And_Retrieve_Product()
        {
            using var context = GetContext();
            var repo = new ProductRepository(context);
            var product = new Product("Product X", "Description", 10m, 10);

            await repo.CreateAsync(product);
            var fetched = await repo.GetByIdAsync(product.Id);

            Assert.NotNull(fetched);
            Assert.Equal("Product X", fetched.Name);
        }

        [Fact(DisplayName = "Should delete product")]
        public async Task Should_Delete_Product()
        {
            using var context = GetContext();
            var repo = new ProductRepository(context);
            var product = new Product("Product X", "Description", 10m, 10);

            await repo.CreateAsync(product);
            var result = await repo.DeleteAsync(product.Id);

            Assert.True(result);
        }
    }
}
