using AutoMapper;
using Backend.Domain.Entities;
using GetSale = Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using CreateSale = Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class SaleEntityToDtoProfile : Profile
{
    public SaleEntityToDtoProfile()
    {
        // Mapeia SaleItem -> SaleItemResultDto
        CreateMap<SaleItem, GetSale.SaleItemResultDto>();
        CreateMap<SaleItem, CreateSale.SaleItemResultDto>();
        CreateMap<CreateSale.SaleItemResultDto, CreateSaleItemResponse>();

        // Mapeia Sale -> GetSaleCommandResult
        CreateMap<Sale, GetSale.GetSaleCommandResult>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList()));

        CreateMap<Sale, CreateSale.CreateSaleCommandResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList()));

            CreateMap<CreateSale.CreateSaleCommandResult, CreateSaleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList()));
    }
}
