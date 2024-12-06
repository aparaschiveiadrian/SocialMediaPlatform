using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Dtos.Account;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Services;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TokenService _tokenService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(UserManager<ApplicationUser> userManager, TokenService tokenService,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            // Check if the ModelState is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Create a new application user
            var applicationUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            // Attempt to create the user
            var createdUser = await _userManager.CreateAsync(applicationUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                // Assign the user to the 'User' role
                var roleResult = await _userManager.AddToRoleAsync(applicationUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(new NewUserDto
                    {
                        Username = applicationUser.UserName,
                        Email = applicationUser.Email,
                        FirstName = applicationUser.FirstName,
                        LastName = applicationUser.LastName,
                        Token = _tokenService.CreateToken(applicationUser)
                    });
                }
                else
                {
                    // Return detailed role assignment errors
                    return BadRequest(roleResult.Errors.Select(e => e.Description));
                }
            }
            else
            {
                // Return detailed creation errors
                return BadRequest(createdUser.Errors.Select(e => e.Description));
            }
        }
        catch (Exception e)
        {
            // Log and return exception details
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());

        if (user == null)
            return Unauthorized("Invalid username!");
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if(!isPasswordValid)
            return Unauthorized("Invalid password!");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Username or password is incorrect!");
        }
        var token = _tokenService.CreateToken(user);
        HttpContext.Response.Cookies.Append("authToken", token, new CookieOptions
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });
        
        return Ok(new NewUserDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        });
    }

    [HttpGet]
    [Route("/user/{userId}")]
    public IActionResult GetUser([FromRoute] string userId)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
        if (user == null)
        {
            return NotFound("User not found!");
        }
        return Ok(new UserDetailsDto
        {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Description = user.Description
        });
    }
}