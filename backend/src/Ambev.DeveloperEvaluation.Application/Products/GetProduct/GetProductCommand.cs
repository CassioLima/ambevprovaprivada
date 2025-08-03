using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Command para obter um produto pelo ID
/// </summary>
public record GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// ID do produto a ser recuperado
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Inicializa uma nova instância de GetProductCommand
    /// </summary>
    /// <param name="id">ID do produto</param>
    public GetProductCommand(Guid id)
    {
        Id = id;
    }
}
