using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Dtos.Conversation;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
public class ConversationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ConversationRepository _convRepo;

    public ConversationController(UserManager<ApplicationUser> userManager, ConversationRepository convRepo)
    {
        _userManager = userManager;
        _convRepo = convRepo;
    }

    [HttpPost]
    [Route("conversation/create")]
    [Authorize]
    public IActionResult CreateConversation([FromBody] CreateConversationDto conversationDto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }
        var conversation = new Conversation() { Name = conversationDto.Name, ModeratorId = userId, };
        _convRepo.CreateConversation(conversation);
        return Ok(new { Name = conversationDto.Name, ModeratorId = userId });
    }
    
}