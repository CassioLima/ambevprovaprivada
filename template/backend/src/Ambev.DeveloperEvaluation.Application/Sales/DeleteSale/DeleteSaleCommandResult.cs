using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}