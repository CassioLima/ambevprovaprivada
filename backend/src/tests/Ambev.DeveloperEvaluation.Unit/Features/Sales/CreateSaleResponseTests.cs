using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Tests.Sales
{
    public class CreateSaleResponseTests
    {
        [Fact(DisplayName = "Deve permitir definir e obter propriedades")]
        public void Should_Set_And_Get_Properties()
        {
            var dto = new CreateSaleResponse
            {
                Id = Guid.NewGuid(),
                SaleNumber = "S123"
            };

            Assert.NotEqual(Guid.Empty, dto.Id);
            Assert.Equal("S123", dto.SaleNumber);
        }
    }
}
