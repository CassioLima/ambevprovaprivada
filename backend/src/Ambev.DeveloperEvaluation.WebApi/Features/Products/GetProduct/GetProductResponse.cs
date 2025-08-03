using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    /// <summary>
    /// Representa a resposta da busca de um produto específico.
    /// </summary>
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
