using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Tests.Sales
{
    public class CreateSaleProfileTests
    {
        [Fact(DisplayName = "CreateSaleProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateSaleProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
