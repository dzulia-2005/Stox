using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock;

public class CreateStockRequestDto
{
    [Required]
    [MaxLength(10,ErrorMessage = "symbol cannot be over 10 characters")]
    public string Symbol { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10,ErrorMessage = "symbols cannot be over 10 characters") ]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    [Range(1,10000)]
    public decimal Purchase { get; set; }
    
    [Required]
    [Range(1,10000)]
    public decimal LastDividend { get; set; }

    [Required]
    [MaxLength(10,ErrorMessage = "symbols can not be over 10 characters")]
    public string Indsutry { get; set; } = string.Empty;
    
    [Required]
    [Range(1,10000)]
    public long MarketCap { get; set; }
    
}