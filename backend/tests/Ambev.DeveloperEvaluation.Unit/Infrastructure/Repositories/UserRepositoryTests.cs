using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private DefaultContext GetContext() =>
            new DefaultContext(new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        [Fact(DisplayName = "Should create and retrieve user by id and email")]
        public async Task Should_Create_And_Retrieve_User()
        {
            using var context = GetContext();
            var repo = new UserRepository(context);
            var user = new User
            {
                Username = "TestUser",
                Email = "test@mail.com",
                Phone = "123456789",
                Password = "P@ssword123",
                CreatedAt = DateTime.UtcNow
            };

            await repo.CreateAsync(user);
            var byId = await repo.GetByIdAsync(user.Id);
            var byEmail = await repo.GetByEmailAsync("test@mail.com");

            Assert.NotNull(byId);
            Assert.NotNull(byEmail);
            Assert.Equal("TestUser", byId.Username);
        }

        [Fact(DisplayName = "Should delete user")]
        public async Task Should_Delete_User()
        {
            using var context = GetContext();
            var repo = new UserRepository(context);
            var user = new User
            {
                Username = "DeleteUser",
                Email = "delete@mail.com",
                Phone = "123456789",
                Password = "P@ssword123",
                CreatedAt = DateTime.UtcNow
            };

            await repo.CreateAsync(user);
            var result = await repo.DeleteAsync(user.Id);

            Assert.True(result);
        }
    }
}
