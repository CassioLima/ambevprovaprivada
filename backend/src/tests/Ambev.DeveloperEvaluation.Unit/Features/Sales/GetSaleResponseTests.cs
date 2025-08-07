using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Tests.Sales
{
    public class GetSaleResponseTests
    {
        [Fact(DisplayName = "Deve permitir definir e obter propriedades")]
        public void Should_Set_And_Get_Properties()
        {
            var dto = new GetSaleResponse
            {
                Id = Guid.NewGuid(),
                Branch = "Main Branch"
            };

            Assert.NotEqual(Guid.Empty, dto.Id);
            Assert.Equal("Main Branch", dto.Branch);
        }
    }
}
