using api.Dtos.Account;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("/api/Account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerdto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registerdto.Username,
                Email = registerdto.Email,
            };

            var createUser = await _userManager.CreateAsync(user,registerdto.Password);
            if (createUser.Succeeded)
            {
                var RoleResult = await _userManager.AddToRoleAsync(user, "user");
                if (RoleResult.Succeeded)
                {
                    return Ok("user created");
                }
                else
                {
                    return StatusCode(500, RoleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createUser.Errors);
            }            
            
            

        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}