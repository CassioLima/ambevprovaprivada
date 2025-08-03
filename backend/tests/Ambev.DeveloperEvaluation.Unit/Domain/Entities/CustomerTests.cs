using System;
using Xunit;
using Backend.Domain.Entities;

namespace Backend.Domain.Tests
{
    public class CustomerTests
    {
        [Fact(DisplayName = "Customer should be created with valid data")]
        public void Customer_Should_Be_Created()
        {
            var customer = new Customer("John Doe", "john@example.com", "(11)90000-0001", "Address 123");
            Assert.NotEqual(Guid.Empty, customer.Id);
            Assert.Equal("John Doe", customer.Name);
            Assert.Equal("john@example.com", customer.Email);
        }

        [Fact(DisplayName = "Customer update should change properties and set UpdatedAt")]
        public void Customer_Update_Should_Work()
        {
            var customer = new Customer("John Doe", "john@example.com", "(11)90000-0001", "Address 123");
            customer.Update("Jane Doe", "jane@example.com", "(11)90000-0002", "New Address");
            Assert.Equal("Jane Doe", customer.Name);
            Assert.NotNull(customer.UpdatedAt);
        }
    }
}
