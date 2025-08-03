using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Repositories
{
    public class SaleItemRepositoryTests
    {
        private DefaultContext GetContext() =>
            new DefaultContext(new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        [Fact(DisplayName = "Should create and retrieve sale item")]
        public async Task Should_Create_And_Retrieve_SaleItem()
        {
            using var context = GetContext();
            var repo = new SaleItemRepository(context);
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 2, 10m, 0);

            await repo.CreateAsync(item);
            var fetched = await repo.GetByIdAsync(item.Id);

            Assert.NotNull(fetched);
            Assert.Equal("Product", fetched.ProductName);
        }

        [Fact(DisplayName = "Should delete sale item")]
        public async Task Should_Delete_SaleItem()
        {
            using var context = GetContext();
            var repo = new SaleItemRepository(context);
            var item = new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 2, 10m, 0);

            await repo.CreateAsync(item);
            var result = await repo.DeleteAsync(item.Id);

            Assert.True(result);
        }
    }
}
