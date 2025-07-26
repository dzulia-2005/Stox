using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Repository;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _repository;
    
    public StockController(ApplicationDbContext context,IStockRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        var stock = await _repository.GetAllAsync(query);
        var stockDto = stock.Select(s => s.ToStockDto());
        return Ok(stockDto); 
    }                     

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await _repository.GetByIdAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        await _repository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
    {
        var stockModel = _repository.UpdateAsync(id, updateStockDto);
        if (stockModel == null)
        {
            return NotFound();
        }
        
        return Ok(stockModel);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _repository.DeleteAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
}