using Backend.Domain.Entities;
using Backend.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Ambev.DeveloperEvaluation.WebApi.Messages;

namespace Backend.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public SalesController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;    
        // _repository = repository; // Uncomment when repository is implemented
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);
        if (sale == null) return NotFound();
        return Ok(sale);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Sale sale)
    {
        //await _repository.AddAsync(sale);
        await _publishEndpoint.Publish(new SaleCreated
        {
            SaleId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            CustomerId = sale.Customer,
            TotalAmount = sale.TotalAmount
        });
        return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);
        if (sale == null) return NotFound();
        sale.Cancel();
        await _repository.UpdateAsync(sale);
        await _publishEndpoint.Publish(new SaleCancelled
        {
            SaleId = sale.Id,
            CancelledAt = DateTime.UtcNow,
            Reason = $"Venda {sale.SaleNumber} foi cancelada por ação de usuário."
        });

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
