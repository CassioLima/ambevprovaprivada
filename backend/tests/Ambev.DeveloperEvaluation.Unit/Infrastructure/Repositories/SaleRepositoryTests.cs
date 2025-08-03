using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Repositories
{
    public class SaleRepositoryTests
    {
        private DefaultContext GetContext() =>
            new DefaultContext(new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        [Fact(DisplayName = "Should create and retrieve sale")]
        public async Task Should_Create_And_Retrieve_Sale()
        {
            using var context = GetContext();
            var repo = new SaleRepository(context);
            var customer = new Customer("John", "john@mail.com", "123", "Address");
            var sale = new Sale("S1", DateTime.UtcNow, customer, "Main Branch", "Cash");

            await repo.CreateAsync(sale);
            var fetched = await repo.GetByIdAsync(sale.Id);

            Assert.NotNull(fetched);
            Assert.Equal(sale.SaleNumber, fetched.SaleNumber);
        }

        [Fact(DisplayName = "Should delete sale")]
        public async Task Should_Delete_Sale()
        {
            using var context = GetContext();
            var repo = new SaleRepository(context);
            var customer = new Customer("John", "john@mail.com", "123", "Address");
            var sale = new Sale("S1", DateTime.UtcNow, customer, "Main Branch", "Cash");

            await repo.CreateAsync(sale);
            var result = await repo.DeleteAsync(sale.Id);

            Assert.True(result);
            Assert.Null(await repo.GetByIdAsync(sale.Id));
        }
    }
}
