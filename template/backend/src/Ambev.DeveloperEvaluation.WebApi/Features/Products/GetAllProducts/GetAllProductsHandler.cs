using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    /// <summary>
    /// Handler responsável por buscar todos os produtos
    /// </summary>
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, List<GetAllProductsResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllProductsResult>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<GetAllProductsResult>>(products);
        }
    }
}
