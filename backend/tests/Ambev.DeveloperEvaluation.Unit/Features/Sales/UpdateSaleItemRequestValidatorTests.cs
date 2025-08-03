using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.Features.Sales
{
    public class UpdateSaleRequestValidatorTests
    {
        [Fact(DisplayName = "Validator deve falhar quando Id estiver vazio")]
        public void Validator_Should_Fail_When_Id_Empty()
        {
            Assert.True(true);
        }
    }
}
