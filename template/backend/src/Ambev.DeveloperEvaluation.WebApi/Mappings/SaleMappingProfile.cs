using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class SaleMappingProfile : Profile
{
    public SaleMappingProfile()
    {
        // Map de SaleItemResultDto -> SaleItemResponse
        CreateMap<SaleItemResultDto, SaleItemResponse>();

        // Map de GetSaleCommandResult -> GetSaleResponse
        CreateMap<GetSaleCommandResult, GetSaleResponse>();
    }
}
