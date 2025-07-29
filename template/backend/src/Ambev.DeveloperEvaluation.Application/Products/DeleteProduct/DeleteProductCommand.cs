using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command para exclusão de um produto
/// </summary>
public record DeleteProductCommand : IRequest<DeleteProductResponse>
{
    /// <summary>
    /// ID do produto a ser excluído
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Inicializa uma nova instância de DeleteProductCommand
    /// </summary>
    /// <param name="id">ID do produto</param>
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
