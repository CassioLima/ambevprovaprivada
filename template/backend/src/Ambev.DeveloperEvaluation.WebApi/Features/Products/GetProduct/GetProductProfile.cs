using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    /// <summary>
    /// Profile para mapeamento de GetProductRequest e GetProductResponse
    /// </summary>
    public class GetProductProfile : Profile
    {
        public GetProductProfile()
        {
            // Corrigido: usa GetProductResult em vez de GetProductCommandResult
            CreateMap<GetProductResult, GetProductResponse>();
            CreateMap<GetProductRequest, GetProductCommand>();
        }
    }
}
