using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    /// <summary>
    /// Validador de CreateProductRequest
    /// </summary>
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("O preço deve ser maior que zero.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("A quantidade em estoque deve ser zero ou maior.");
        }
    }
}
