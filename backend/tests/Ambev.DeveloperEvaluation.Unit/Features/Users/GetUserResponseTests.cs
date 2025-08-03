using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Tests.Users
{
    public class GetUserResponseTests
    {
        [Fact(DisplayName = "Deve permitir definir e obter propriedades")]
        public void Should_Set_And_Get_Properties()
        {
            var dto = new GetUserResponse
            {
                Id = Guid.NewGuid(),
                Name = "User Teste"
            };

            Assert.NotEqual(Guid.Empty, dto.Id);
            Assert.Equal("User Teste", dto.Name);
        }
    }
}
