using api.Dtos.Comment;

namespace api.Dtos.Stock;

public class StockDto
{
    public int Id { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    public decimal Purchase { get; set; }

    public decimal LastDividend { get; set; }

    public string Indsutry { get; set; } = string.Empty;

    public long MarketCap { get; set; }
    public List<CommentDto> Comments { get; set; } 
}