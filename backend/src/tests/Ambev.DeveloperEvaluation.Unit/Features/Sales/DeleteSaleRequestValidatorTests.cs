using Xunit;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

namespace Ambev.DeveloperEvaluation.Tests.Sales
{
    public class DeleteSaleRequestValidatorTests
    {
        [Fact(DisplayName = "Deve falhar quando Id estiver vazio")]
        public async Task Should_Fail_When_Id_Empty()
        {
            var validator = new DeleteSaleRequestValidator();
            var request = new DeleteSaleRequest { Id = Guid.Empty };

            var result = await validator.ValidateAsync(request);

            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Deve passar quando Id válido")]
        public async Task Should_Pass_When_Id_Valid()
        {
            var validator = new DeleteSaleRequestValidator();
            var request = new DeleteSaleRequest { Id = Guid.NewGuid() };

            var result = await validator.ValidateAsync(request);

            Assert.True(result.IsValid);
        }
    }
}
