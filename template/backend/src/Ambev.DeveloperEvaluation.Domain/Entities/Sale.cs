namespace Backend.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string Customer { get; private set; }
        public string Branch { get; private set; }
        public bool IsCancelled { get; private set; }
        public decimal TotalAmount => Items.Sum(i => i.Total);

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items;

        private Sale() { }

        public Sale(string saleNumber, DateTime saleDate, string customer, string branch)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            Customer = customer;
            Branch = branch;
        }

        public void AddItem(Guid productId, string description, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (quantity > 20)
                throw new InvalidOperationException("It's not possible to sell more than 20 identical items.");

            // SaleItem já aplica automaticamente o desconto conforme a quantidade
            var item = new SaleItem(productId, description, quantity, unitPrice);
            _items.Add(item);
        }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public void CancelItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(x => x.ProductId == itemId);
            if (item != null)
            {
                // Estratégia: cancelar significa zerar quantidade e desconto
                item.UpdateQuantity(0);
            }
        }
    }
}
