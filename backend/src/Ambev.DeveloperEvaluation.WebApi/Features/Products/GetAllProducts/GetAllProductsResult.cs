

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts
{
    /// <summary>
    /// Resultado do retorno de um produto individual
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
