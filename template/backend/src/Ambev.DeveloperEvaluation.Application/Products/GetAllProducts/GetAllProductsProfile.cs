using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsProfile : Profile
    {
        public GetAllProductsProfile()
        {
            CreateMap<Product, GetAllProductsResult>();
        }
    }
}
