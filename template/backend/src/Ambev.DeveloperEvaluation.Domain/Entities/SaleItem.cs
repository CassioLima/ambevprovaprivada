using System;

namespace Backend.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Guid SaleId { get; private set; }
        public Sale Sale { get; private set; }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal Total => (UnitPrice * Quantity) - DiscountAmount;
        public decimal DiscountAmount => (UnitPrice * Quantity) * (DiscountPercentage / 100);

        private SaleItem() { }

        public SaleItem(Guid saleId, Guid productId, string productName, int quantity, decimal unitPrice, decimal discountPercentage)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (quantity > 20)
                throw new InvalidOperationException("It's not possible to sell more than 20 identical items.");

            Id = Guid.NewGuid();
            SaleId = saleId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountPercentage = discountPercentage;

            ApplyDiscount();
        }

        public SaleItem(Guid id, Guid saleId, Guid productId, string productName, int quantity, decimal unitPrice, decimal discountPercentage)
        {
            Id = id;
            SaleId = saleId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountPercentage = discountPercentage;

        }

        private void ApplyDiscount()
        {
            if (Quantity < 4)
                DiscountPercentage = 0;
            else if (Quantity >= 4 && Quantity < 10)
                DiscountPercentage = 10;
            else if (Quantity >= 10 && Quantity <= 20)
                DiscountPercentage = 20;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (newQuantity > 20)
                throw new InvalidOperationException("It's not possible to sell more than 20 identical items.");

            Quantity = newQuantity;
            ApplyDiscount();
        }
    }
}
