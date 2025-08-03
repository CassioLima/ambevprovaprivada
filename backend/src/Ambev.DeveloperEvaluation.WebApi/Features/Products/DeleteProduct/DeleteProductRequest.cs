using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{
    /// <summary>
    /// Representa o corpo da requisição para deletar um produto.
    /// </summary>
    public class DeleteProductRequest
    {
        /// <summary>
        /// ID do produto a ser deletado.
        /// </summary>
        public Guid Id { get; set; }
    }
}
