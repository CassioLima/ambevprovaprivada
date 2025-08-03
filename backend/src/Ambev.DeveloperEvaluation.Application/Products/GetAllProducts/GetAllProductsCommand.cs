using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    /// <summary>
    /// Query para buscar todos os produtos
    /// </summary>
    public class GetAllProductsCommand : IRequest<List<GetAllProductsResult>>
    {
    }
}
