using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{
    /// <summary>
    /// Profile de mapeamento para DeleteProductRequest → DeleteProductCommand
    /// </summary>
    public class DeleteProductProfile : Profile
    {
        public DeleteProductProfile()
        {
            CreateMap<DeleteProductRequest, DeleteProductCommand>()
                .ConvertUsing(src => new DeleteProductCommand(src.Id));
        }
    }
}
