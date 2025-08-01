using api.Dtos.Account;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("/api/Account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;
    public AccountController(UserManager<User> userManager,ITokenService tokenService,SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userManager.Users.FirstOrDefault(x => x.UserName == loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("username is not valid"); 
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("username/password is not valid");
            }

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost("register")]
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
                var RoleResult = await _userManager.AddToRoleAsync(user, "User");
                if (RoleResult.Succeeded)
                {
                    return Ok(
                            new NewUserDto
                            {
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)
                            }
                        );
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