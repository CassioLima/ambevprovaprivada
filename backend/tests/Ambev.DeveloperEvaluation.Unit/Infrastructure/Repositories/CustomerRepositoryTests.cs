using Microsoft.EntityFrameworkCore;
using Xunit;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        private DefaultContext GetContext() =>
            new DefaultContext(new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options);

        [Fact(DisplayName = "Should create and retrieve customer")]
        public async Task Should_Create_And_Retrieve_Customer()
        {
            using var context = GetContext();
            var repo = new CustomerRepository(context);
            var customer = new Customer("John Doe", "john@mail.com", "123", "Address");

            await repo.CreateAsync(customer);
            var fetched = await repo.GetByIdAsync(customer.Id);

            Assert.NotNull(fetched);
            Assert.Equal("John Doe", fetched.Name);
        }

        [Fact(DisplayName = "Should delete customer")]
        public async Task Should_Delete_Customer()
        {
            using var context = GetContext();
            var repo = new CustomerRepository(context);
            var customer = new Customer("John Doe", "john@mail.com", "123", "Address");

            await repo.CreateAsync(customer);
            var result = await repo.DeleteAsync(customer.Id);

            Assert.True(result);
        }
    }
}
