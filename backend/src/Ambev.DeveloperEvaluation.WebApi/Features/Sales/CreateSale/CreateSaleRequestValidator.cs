using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.SaleNumber).NotEmpty();
        RuleFor(x => x.SaleDate).NotEmpty();
        RuleFor(x => x.Branch).NotEmpty();
        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId).NotEmpty();
            items.RuleFor(i => i.Quantity).GreaterThan(0);
        });
    }
}
