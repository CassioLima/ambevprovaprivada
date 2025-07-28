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
        public decimal TotalAmount => _items.Sum(i => i.Total);

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        private Sale() { }

        public Sale(string saleNumber, DateTime saleDate, Guid customerId, string branch, string paymentMethod)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber ?? throw new ArgumentNullException(nameof(saleNumber));
            SaleDate = saleDate;
            CustomerId = customerId;
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            PaymentMethod = paymentMethod ?? "Uninformed";
            Status = "Pending";
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            if (Status == "Cancelled")
                throw new InvalidOperationException("Cannot add items to a cancelled sale.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (quantity > 20)
                throw new InvalidOperationException("It's not possible to sell more than 20 identical items.");

            var item = new SaleItem(Id, productId, productName, quantity, unitPrice);
            _items.Add(item);
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
    }
}
