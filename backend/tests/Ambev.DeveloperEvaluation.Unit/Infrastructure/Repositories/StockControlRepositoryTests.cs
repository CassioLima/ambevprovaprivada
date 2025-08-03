using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Repositories
{
    public class StockControlRepositoryTests
    {
        private DefaultContext GetContext() =>
            new DefaultContext(new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        [Fact(DisplayName = "Should create and retrieve stock control")]
        public async Task Should_Create_And_Retrieve_StockControl()
        {
            using var context = GetContext();
            var repo = new StockControlRepository(context);
            var stock = new StockControl(Guid.NewGuid(), 10, "Entrada");

            await repo.CreateAsync(stock);
            var fetched = await repo.GetByIdAsync(stock.Id);

            Assert.NotNull(fetched);
            Assert.Equal(10, fetched.Quantity);
        }

        [Fact(DisplayName = "Should delete stock control")]
        public async Task Should_Delete_StockControl()
        {
            using var context = GetContext();
            var repo = new StockControlRepository(context);
            var stock = new StockControl(Guid.NewGuid(), 10, "Entrada");

            await repo.CreateAsync(stock);
            var result = await repo.DeleteAsync(stock.Id);

            Assert.True(result);
        }
    }
}
