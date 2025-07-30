using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Tests.Products
{
    public class GetProductResponseTests
    {
        [Fact(DisplayName = "Deve permitir definir e obter propriedades")]
        public void Should_Set_And_Get_Properties()
        {
            var dto = new GetProductResponse
            {
                Id = Guid.NewGuid(),
                Name = "Produto Teste"
            };

            Assert.NotEqual(Guid.Empty, dto.Id);
            Assert.Equal("Produto Teste", dto.Name);
        }
    }
}
