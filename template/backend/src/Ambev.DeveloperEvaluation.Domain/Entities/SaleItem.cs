namespace Backend.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; } // Adicione uma chave primária se não tiver
        public Guid SaleId { get; private set; }   // <- FK explícita
        public Sale Sale { get; private set; }     // <- Navegação inversa

        public Guid ProductId { get; private set; }
        public string ProductDescription { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal Total => (UnitPrice * Quantity) - DiscountAmount;
        public decimal DiscountAmount => (UnitPrice * Quantity) * (DiscountPercentage / 100);

        protected SaleItem() { }

        public SaleItem(Guid saleId, Guid productId, string description, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (quantity > 20)
                throw new InvalidOperationException("It's not possible to sell more than 20 identical items.");

            SaleId = saleId;
            ProductId = productId;
            ProductDescription = description;
            Quantity = quantity;
            UnitPrice = unitPrice;

            ApplyDiscount();
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
