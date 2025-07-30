using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Products
{
    public class GetProductRequestValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando Id estiver vazio")]
        public void Validator_Should_Fail_When_Id_Empty()
        {
            var validator = new GetProductRequestValidator();
            var result = validator.Validate(new GetProductRequest { Id = Guid.Empty });

            Assert.False(result.IsValid);
        }
    }
}
