using Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public string Branch { get; private set; }
        public string PaymentMethod { get; private set; }
        public string Status { get; private set; }   // Pending, Paid, Cancelled
        public decimal TotalAmount { get; private set; }

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        private Sale() { }

        public Sale(string saleNumber, DateTime saleDate, Customer customer, string branch, string paymentMethod)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber ?? throw new ArgumentNullException(nameof(saleNumber));
            SaleDate = saleDate;
            CustomerId = customer.Id;
            Customer = customer;
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            PaymentMethod = paymentMethod ?? "Uninformed";
            Status = "Pending";
            TotalAmount = 0;
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice, decimal discountPercentage)
        {
            if (Status == "Cancelled")
                throw new InvalidOperationException("Cannot add items to a cancelled sale.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (quantity > 20)
                throw new InvalidOperationException("It's not possible to sell more than 20 identical items.");

            var item = new SaleItem(Id, productId, productName, quantity, unitPrice, discountPercentage);
            _items.Add(item);

            RecalculateTotal();
        }

        public void RemoveItem(Guid itemId, Guid productId, string productName, int quantity, decimal unitPrice, decimal discountPercentage)
        {
            if (Status == "Cancelled")
                throw new InvalidOperationException("Cannot add items to a cancelled sale.");

            var item = new SaleItem(itemId, Id, productId, productName, quantity, unitPrice, discountPercentage);
            _items.Remove(item);

            RecalculateTotal();
        }

        public void UpdateItem(Guid itemId, Guid productId, string productName, int quantity, decimal unitPrice, decimal discountPercentage)
        {
            if (Status == "Cancelled")
                throw new InvalidOperationException("Cannot add items to a cancelled sale.");

            var item = new SaleItem(itemId, Id, productId, productName, quantity, unitPrice, discountPercentage);
            _items.Remove(item);

            RecalculateTotal();
        }

        public void Cancel()
        {
            if (Status == "Cancelled")
                throw new InvalidOperationException("Sale is already cancelled.");

            Status = "Cancelled";
        }

        public void Complete()
        {
            if (!_items.Any())
                throw new InvalidOperationException("Cannot complete a sale with no items.");

            if (Status == "Cancelled")
                throw new InvalidOperationException("Cannot complete a cancelled sale.");

            Status = "Paid";
        }

        private void RecalculateTotal()
        {
            TotalAmount = _items.Sum(i =>
                (i.Quantity * i.UnitPrice) - (i.Quantity * i.UnitPrice * i.DiscountPercentage / 100)
            );
        }
    }
}
