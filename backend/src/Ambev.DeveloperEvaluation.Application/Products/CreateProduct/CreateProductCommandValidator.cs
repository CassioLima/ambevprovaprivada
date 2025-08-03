using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validação do comando de criação de produto.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().Length(3, 100);
        RuleFor(p => p.Description).MaximumLength(500);
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.StockQuantity).GreaterThanOrEqualTo(0);
    }
}
