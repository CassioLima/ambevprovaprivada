using System;

namespace Backend.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private readonly List<Sale> _sales = new();
        public IReadOnlyCollection<Sale> Sales => _sales;

        private Customer() { }

        public Customer(string name, string email, string phone, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required", nameof(email));

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string email, string phone, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required", nameof(email));

            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
