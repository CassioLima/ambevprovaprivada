using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsProfile : Profile
    {
        public GetAllProductsProfile()
        {
            CreateMap<GetProductResult, GetProductResponse>();
            CreateMap<GetProductRequest, GetProductCommand>();
            CreateMap<GetAllProductsResult, GetProductResponse>();
        }
    }
}
