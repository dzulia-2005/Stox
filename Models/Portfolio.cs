using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Portfolios")]
public class Portfolio
{
    public string UserId { get; set; }
    public int StockId { get; set; }
    
    public User User { get; set; }
    public Stock Stock { get; set; }
}