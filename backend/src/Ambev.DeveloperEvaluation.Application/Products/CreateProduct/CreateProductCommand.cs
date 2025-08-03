using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command para criação de um novo produto.
/// </summary>
public class CreateProductCommand : IRequest<CreateProductResult>
{
    /// <summary>Nome do produto.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Descrição do produto.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Preço do produto.</summary>
    public decimal Price { get; set; }

    /// <summary>Quantidade inicial em estoque.</summary>
    public int StockQuantity { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
