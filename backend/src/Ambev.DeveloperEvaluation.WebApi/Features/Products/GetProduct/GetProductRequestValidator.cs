using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    /// <summary>
    /// Validador de GetProductRequest
    /// </summary>
    public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
    {
        public GetProductRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do produto é obrigatório.");
        }
    }
}
