using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;
    
    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();

        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockDto)
    {
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingStock==null)
        {
            return null;
        }

        existingStock.Symbol = updateStockDto.Symbol;
        existingStock.CompanyName = updateStockDto.CompanyName;
        existingStock.Purchase = updateStockDto.Purchase;
        existingStock.LastDividend = updateStockDto.LastDividend;
        existingStock.Indsutry = updateStockDto.Indsutry;
        existingStock.MarketCap = updateStockDto.MarketCap;

        await _context.SaveChangesAsync();
        return existingStock;

    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel==null)
        {
            return null;
        }
        _context.Stocks.Remove(stockModel);
        _context.SaveChangesAsync();
        
        return stockModel;
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(x=>x.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.Include(x=>x.Comments).ToListAsync();
    }

    public Task<bool> StockExists(int id)
    {
        return _context.Stocks.AnyAsync(x => x.Id == id);
    }
    
}