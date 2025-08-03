using System;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    /// <summary>
    /// DTO de resultado para listagem de produtos
    /// </summary>
    public class GetAllProductsResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
