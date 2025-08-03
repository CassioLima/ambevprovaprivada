using System;
using System.Collections.Generic;

namespace Backend.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<StockControl> _stockMovements = new();
        public IReadOnlyCollection<StockControl> StockMovements => _stockMovements.AsReadOnly();

        private Product() { }

        public Product(string name, string description, decimal price, int stockQuantity)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CreatedAt = DateTime.UtcNow;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0");
            StockQuantity += quantity;
            _stockMovements.Add(new StockControl(Id, quantity, "Entrada"));
        }

        public void DecreaseStock(int quantity)
        {
            ValidateStock(quantity);
            StockQuantity -= quantity;
        }

        public void ValidateStock(int quantity)
        {
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative");
            if (quantity > StockQuantity) throw new InvalidOperationException("Insufficient stock");
        }       
    }
}
