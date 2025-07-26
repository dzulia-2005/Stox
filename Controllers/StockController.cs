using api.Data;
using api.Dtos.Stock;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.ToList()
            .Select(s=>s.ToStockDto());
        
        return Ok(stocks); 
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stocks.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
    {
        var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if (stockModel==null)
        {
            return NotFound();
        }

        stockModel.Symbol = updateStockDto.Symbol;
        stockModel.CompanyName = updateStockDto.CompanyName;
        stockModel.Purchase = updateStockDto.Purchase;
        stockModel.LastDividend = updateStockDto.LastDividend;
        stockModel.Indsutry = updateStockDto.Indsutry;
        stockModel.MarketCap = updateStockDto.MarketCap;

        _context.SaveChanges();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if (stock == null)
        {
            return NotFound();
        }
        _context.Stocks.Remove(stock);
        _context.SaveChanges();

        return NoContent();
    }
    
}