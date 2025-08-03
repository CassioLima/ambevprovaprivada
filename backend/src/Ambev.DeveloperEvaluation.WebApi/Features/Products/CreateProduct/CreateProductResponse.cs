using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    /// <summary>
    /// Representa a resposta de um produto criado com sucesso.
    /// </summary>
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
