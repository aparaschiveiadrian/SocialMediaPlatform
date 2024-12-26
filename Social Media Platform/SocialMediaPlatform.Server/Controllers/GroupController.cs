using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Dtos.Group;
using SocialMediaPlatform.Server.Mappers;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
public class GroupController : ControllerBase
{
    private readonly GroupRepository _groupRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    public GroupController(UserManager<ApplicationUser> userManager, GroupRepository groupRepo)
    {
        _groupRepo = groupRepo;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("group/create")]
    [Authorize]
    public IActionResult CreateGroup([FromBody] CreateGroupDto createGroupDto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }
        var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return Unauthorized();
        }
        var group = createGroupDto.ToGroupFromCreateDto(userId);
        group.Users.Add(user);
        
        _groupRepo.AddGroup(group);
        
        return Ok(new
        {
            Name = group.Name,
            ModeratorId = group.ModeratorId
        });
    }
    
    
}