using Microsoft.AspNetCore.Builder;

namespace Ambev.DeveloperEvaluation.Common.Security
{
    public static class AuthenticationExtension
    {
        public static WebApplicationBuilder AddAuthenticationExtension(this WebApplicationBuilder builder)
        {
            return builder;
        }
    }
}
