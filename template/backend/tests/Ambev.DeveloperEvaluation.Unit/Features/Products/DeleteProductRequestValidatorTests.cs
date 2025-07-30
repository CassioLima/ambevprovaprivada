using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Products
{
    public class DeleteProductRequestValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando Id estiver vazio")]
        public void Validator_Should_Fail_When_Id_Empty()
        {
            var validator = new DeleteProductRequestValidator();
            var result = validator.Validate(new DeleteProductRequest { Id = Guid.Empty });

            Assert.False(result.IsValid);
        }
    }
}
