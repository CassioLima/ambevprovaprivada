using System;

namespace Backend.Domain.Entities
{
    public class StockControl
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string MovementType { get; private set; } // Entrada ou Saída
        public int Quantity { get; private set; }
        public DateTime MovementDate { get; private set; }

        private StockControl() { }

        public StockControl(Guid productId, int quantity, string movementType)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            MovementType = movementType;
            MovementDate = DateTime.UtcNow;
        }
    }
}
