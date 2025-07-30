using Xunit;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.Tests.Products
{
    public class DeleteProductProfileTests
    {
        [Fact(DisplayName = "DeleteProductProfile deve ter configuração válida")]
        public void Profile_Should_Have_Valid_Configuration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DeleteProductProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
