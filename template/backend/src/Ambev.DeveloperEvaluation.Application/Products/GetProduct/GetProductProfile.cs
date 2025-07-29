using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Mapeamento entre Product e GetProductResult
/// </summary>
public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>();
    }
}
