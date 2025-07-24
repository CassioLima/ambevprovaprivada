using Backend.Domain.Entities;
using Backend.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleRepository _repository;

    public SalesController(ISaleRepository repository)
    {
        _repository = repository;
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
        await _repository.AddAsync(sale);
        return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);
        if (sale == null) return NotFound();
        sale.Cancel();
        await _repository.UpdateAsync(sale);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
