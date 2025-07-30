using Xunit;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Tests.Sales
{
    public class CreateSaleRequestValidatorTests
    {
        [Fact(DisplayName = "Deve falhar se Branch estiver vazio")]
        public async Task Should_Fail_When_Branch_Empty()
        {
            var validator = new CreateSaleRequestValidator();
            var request = new CreateSaleRequest { Branch = "" };

            var result = await validator.ValidateAsync(request);

            Assert.False(result.IsValid);
        }
        
        
        [Fact(DisplayName = "Deve passar quando dados corretos")]
        public async Task Should_Pass_When_Valid()
        {
            var validator = new CreateSaleRequestValidator();
            var request = new CreateSaleRequest
            {
                SaleNumber = "S1",
                SaleDate = DateTime.UtcNow,
                Branch = "Main Branch",
                CustomerId = Guid.NewGuid(), 
                Items = new List<SaleItemRequest>
        {
            new SaleItemRequest
            {
                ProductId = Guid.NewGuid(),
                Quantity = 2
            }
        }
            };

            var result = await validator.ValidateAsync(request);

            Assert.True(result.IsValid);
        }
    }
}
