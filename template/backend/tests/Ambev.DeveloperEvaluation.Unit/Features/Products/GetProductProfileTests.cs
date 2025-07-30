using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Tests.Products
{
    public class GetProductProfileTests
    {
        [Fact(DisplayName = "GetProductProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GetProductProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
