using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Products
{
    public class CreateProductRequestValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando nome estiver vazio")]
        public void Validator_Should_Fail_When_Name_Empty()
        {
            var validator = new CreateProductRequestValidator();
            var result = validator.Validate(new CreateProductRequest { Name = "" });

            Assert.False(result.IsValid);
        }
    }
}
