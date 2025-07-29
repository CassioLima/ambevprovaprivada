using MediatR;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MassTransit;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class UpdateSaleItemCommandHandler : IRequestHandler<UpdateSaleItemCommand>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateSaleItemCommandHandler(IMapper mapper, IPublishEndpoint publishEndpoint, ISaleRepository saleRepository, ISaleItemRepository saleItemRepository)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
    }


    public async Task<Unit> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
    {
        var saleItem = await _saleItemRepository.GetByIdAsync(request.ItemId, cancellationToken);

        if (saleItem == null)
            throw new KeyNotFoundException("Sale not found");

        await _saleItemRepository.UpdateAsync(saleItem, cancellationToken);

        return Unit.Value;
    }

    Task IRequestHandler<UpdateSaleItemCommand>.Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
