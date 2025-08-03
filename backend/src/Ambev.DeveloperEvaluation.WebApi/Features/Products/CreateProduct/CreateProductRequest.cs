using System;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    /// <summary>
    /// Representa o corpo da requisição para criar um novo produto.
    /// </summary>
    public class CreateProductRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser zero ou maior.")]
        public int StockQuantity { get; set; }
    }
}
