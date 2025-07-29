namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Resultado da operação de obter produto
/// </summary>
public class GetProductResult
{
    /// <summary>
    /// ID do produto
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome do produto
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do produto
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Preço do produto
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Quantidade em estoque
    /// </summary>
    public int StockQuantity { get; set; }
}
