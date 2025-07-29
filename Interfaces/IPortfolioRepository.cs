using api.Models;

namespace api.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetPortfolio(User user);
    Task<Portfolio> CreateAsync(Portfolio portfolio);
}