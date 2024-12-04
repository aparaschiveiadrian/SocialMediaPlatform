using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Dtos.Account;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
                Email = registerDto.Email
            };

            // Attempt to create the user
            var createdUser = await _userManager.CreateAsync(applicationUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                // Assign the user to the 'User' role
                var roleResult = await _userManager.AddToRoleAsync(applicationUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok("User created");
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

}