using AutoMapper;
using Backend.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class SaleEntityToDtoProfile : Profile
{
    public SaleEntityToDtoProfile()
    {
        // Mapeia SaleItem -> SaleItemResultDto
        CreateMap<SaleItem, SaleItemResultDto>();

        // Mapeia Sale -> GetSaleCommandResult
        CreateMap<Sale, GetSaleCommandResult>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList()));
    }
}
