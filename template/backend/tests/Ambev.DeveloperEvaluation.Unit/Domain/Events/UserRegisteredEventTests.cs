using System;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Tests.Events
{
    public class UserRegisteredEventTests
    {
        [Fact(DisplayName = "UserRegisteredEvent should hold provided user")]
        public void UserRegisteredEvent_Should_Hold_User()
        {
            var user = new User
            {
                Username = "testuser",
                Email = "user@example.com",
                Phone = "(11)99999-9999",
                Password = "StrongP@ssword1",
                CreatedAt = DateTime.UtcNow
            };

            var evt = new UserRegisteredEvent(user);

            Assert.Equal(user, evt.User);
            Assert.Equal("testuser", evt.User.Username);
        }
    }
}
