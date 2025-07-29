using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    /// <summary>
    /// Representa a requisição para buscar um produto específico.
    /// </summary>
    public class GetProductRequest
    {
        public Guid Id { get; set; }
    }
}
