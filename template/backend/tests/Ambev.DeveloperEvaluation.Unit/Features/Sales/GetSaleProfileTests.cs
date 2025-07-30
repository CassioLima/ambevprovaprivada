using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            // Mapeamento de itens
            CreateMap<SaleItemResultDto, GetSaleItemResponse>();

            // Mapeamento de venda completa
            CreateMap<GetSaleCommandResult, GetSaleResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            // Mapeamento de request para command
            CreateMap<GetSaleRequest, GetSaleCommand>();
        }
    }
}
