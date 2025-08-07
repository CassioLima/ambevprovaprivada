using MediatR;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Cache;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    /// <summary>
    /// Handler responsável por buscar todos os produtos
    /// </summary>
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, List<GetAllProductsResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;

        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper, IRedisCacheService redisCacheService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
        }

        public async Task<List<GetAllProductsResult>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            const string cacheKey = "products:all";

            // 1. Tenta buscar no cache
            var cachedProducts = await _redisCacheService.GetAsync<List<GetAllProductsResult>>(cacheKey, cancellationToken);
            if (cachedProducts is not null)
                return cachedProducts;

            // 2. Se não tem no cache, busca no repositório
            var products = await _productRepository.GetAllAsync(cancellationToken);

            // 3. Mapeia para DTO de resultado
            var mappedResult = _mapper.Map<List<GetAllProductsResult>>(products);

            // 4. Salva no cache por 5 minutos
            await _redisCacheService.SetAsync(cacheKey, mappedResult, TimeSpan.FromMinutes(5), cancellationToken);

            // 5. Retorna o resultado
            return mappedResult;

        }
    }
}
