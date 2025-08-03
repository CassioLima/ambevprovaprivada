using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommandHandler : IRequestHandler<GetSaleCommand, GetSaleCommandResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSaleCommandResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            return _mapper.Map<GetSaleCommandResult>(sale);
        }
    }
}
