using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.Tests.Products
{
    public class CreateProductProfileTests
    {
        [Fact(DisplayName = "CreateProductProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateProductProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
