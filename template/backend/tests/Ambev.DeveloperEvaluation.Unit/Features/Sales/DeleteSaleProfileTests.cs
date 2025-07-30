using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

namespace Ambev.DeveloperEvaluation.Tests.Sales
{
    public class DeleteSaleProfileTests
    {
        [Fact(DisplayName = "DeleteSaleProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeleteSaleProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
