﻿using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
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
        return Ok(new NewUserDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = token
        });
    }

     [HttpGet]
     [Route("/getUserById/{userId}")]
     public IActionResult GetUserById([FromRoute] string userId)
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
    [HttpGet]
    [Route("/user/{username}")]
    public IActionResult GetUser([FromRoute] string username)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.UserName == username);
        if (user == null)
        {
            return NotFound("A user with this username could not be found!");
        } 
        return Ok(new UserDetailsDto
             {
                 Username = user.UserName,
                 FirstName = user.FirstName,
                 LastName = user.LastName,
                 Description = user.Description,
                 IsPrivate = user.IsPrivate,
                 ProfilePictureUrl = user.ProfilePictureUrl
             });
    }
    [HttpGet]
    [Route("/getUserIdByUsername/{username}")]
    public IActionResult GetUserIdByUsername([FromRoute] string username)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.UserName == username);
        if (user == null)
        {
            return NotFound("A user with this username could not be found!");
        } 
        return Ok(new { userId = user.Id }); 
    }

    [HttpPut]
    [Route("/changePrivacy")]
    [Authorize]
    public async Task<IActionResult> ChangePrivacy()
    {
        var userId = _userManager.GetUserId(User);
        var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
        if (user == null)
        {
            return NotFound("User could not be found!");
        }
        user.IsPrivate = !user.IsPrivate;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest("Failed to update user privacy.");
        }
        return Ok(user);
    }
    [HttpPut]
    [Route("/user/edit")]
    [Authorize]
    public async Task<IActionResult> EditUser([FromBody] UserEditDto userEditDto)
    {
        var userId = _userManager.GetUserId(User);
        var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Update Profile Picture if provided
        if (!string.IsNullOrWhiteSpace(userEditDto.ProfilePictureUrl))
        {
            user.ProfilePictureUrl = userEditDto.ProfilePictureUrl;
        }

        // Update First Name if provided
        if (!string.IsNullOrWhiteSpace(userEditDto.FirstName))
        {
            user.FirstName = userEditDto.FirstName;
        }

        // Update Last Name if provided
        if (!string.IsNullOrWhiteSpace(userEditDto.LastName))
        {
            user.LastName = userEditDto.LastName;
        }

        // Update Description if provided
        if (userEditDto.Description != null)
        {
            user.Description = userEditDto.Description;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest("Failed to update user details.");
        }

        return Ok(new
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Description = user.Description,
            ProfilePictureUrl = user.ProfilePictureUrl
        });
    }

    
  

    [HttpGet]
    [Route("user/allUsers")]
    public IActionResult GetAllUsers()
    {
        var users = _userManager.Users
            .Select(user => new UserSearchDto()
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            })
            .ToList();

        return Ok(users);
    }
    
    [HttpPut]
    [Route("/user/edit-profile-picture")]
    [Authorize]
    public async Task<IActionResult> EditProfilePicture([FromBody] string profilePictureUrl)
    {
        var userId = _userManager.GetUserId(User);
        var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (string.IsNullOrWhiteSpace(profilePictureUrl))
        {
            return BadRequest("Profile picture URL cannot be empty.");
        }

        user.ProfilePictureUrl = profilePictureUrl;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest("Failed to update profile picture.");
        }

        return Ok(new { ProfilePictureUrl = user.ProfilePictureUrl });
    }

    
    [HttpGet("isAdmin")]
    [Authorize]
    public async Task<IActionResult> IsAdmin()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
    
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        return Ok(new { IsAdmin = isAdmin });
    }
}