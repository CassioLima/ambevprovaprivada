using Xunit;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.Tests.Users
{
    public class CreateUserResponseTests
    {
        [Fact(DisplayName = "Deve permitir definir e obter propriedades")]
        public void Should_Set_And_Get_Properties()
        {
            var dto = new CreateUserResponse
            {
                Id = Guid.NewGuid(),
                Name = "User Teste"
            };

            Assert.NotEqual(Guid.Empty, dto.Id);
            Assert.Equal("User Teste", dto.Name);
        }
    }
}
