using api.Dtos.Stock;
using api.Models;

namespace api.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            MarketCap = stockModel.MarketCap,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDividend = stockModel.LastDividend,
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
    {
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            LastDividend = stockDto.LastDividend,
            MarketCap = stockDto.MarketCap,
            Purchase = stockDto.Purchase,
            Indsutry = stockDto.Indsutry
        };
    }
}