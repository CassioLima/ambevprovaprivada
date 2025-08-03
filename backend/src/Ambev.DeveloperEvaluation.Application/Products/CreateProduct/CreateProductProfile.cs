using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Backend.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Profile de mapeamento de Produto.
/// </summary>
public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductResult>();
    }
}
