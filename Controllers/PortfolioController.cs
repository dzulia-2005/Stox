using api.Interfaces;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Extension;

namespace api.Controllers;


[Route("api/Account")]
[ApiController]

public class PortfolioController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IStockRepository _stockrepo;
    private readonly IPortfolioRepository _portfolioRepo;
    
    public PortfolioController(UserManager<User> userManager,IStockRepository stockrepo,IPortfolioRepository portfolioRepo)
    {
        _userManager = userManager;
        _stockrepo = stockrepo;
        _portfolioRepo = portfolioRepo;
    }

    [HttpGet]
    [Authorize]

    public async Task<ActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepo.GetPortfolio(user);
        
        return Ok(userPortfolio);

    }

    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult> AddPortfolio(string symbol)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username);
        var stock = await _stockrepo.GetBySymbolAsync(symbol);

        if (stock == null) return BadRequest("stock not found");
        var userPortfolio = await _portfolioRepo.GetPortfolio(user);
        if (userPortfolio.Any(e=>e.Symbol.ToLower()==symbol.ToLower()))
        {
            return BadRequest("cannot add same stock to portfolio");
        }

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            UserId = user.Id
        };

        await _portfolioRepo.CreateAsync(portfolioModel);
        if (portfolioModel == null)
        {
            return StatusCode(500,"could not created");
        }
        else
        {
            return Created();
        }
    }

    [HttpDelete("delete")]
    [Authorize]

    public async Task<ActionResult> DeletePortfolio(string symbol)
    {
        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _portfolioRepo.GetPortfolio(user);

        var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower()==symbol.ToLower());
        if (filteredStock.Count() == 1)
        {
            await _portfolioRepo.DeleteAsync(user, symbol);
        }
        else
        {
            return BadRequest("stock not in your portfolio");
        }

        return Ok();
    }
    
}