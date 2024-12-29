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

    [HttpPost]
    [Route("conversation/join/{conversationId}")]
    [Authorize]
    public IActionResult JoinConversationRequest(int conversationId)
    {
        var userId = _userManager.GetUserId(User);
        var conversation = _convRepo.GetConversationById(conversationId);
        if (conversation == null)
        {
            return NotFound();
        }
        if (userId == null)
        {
            return Unauthorized();
        }
        _convRepo.JoinConversation(conversation, userId);
        return Ok(conversation.Name);
    }

    [HttpPut]
    [Route("conversation/accept/request/{username}/{conversationId}")]
    [Authorize]
    public IActionResult AcceptRequest(string username, int conversationId)
    {
        var currentUserId = _userManager.GetUserId(User);
        if (currentUserId == null)
        {
            return Unauthorized();
        }
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);
        if (user == null)
        {
            return NotFound();
        }

        var userId = user.Id;
        var conversation = _convRepo.GetConversationById(conversationId);
        if (conversation.ModeratorId != currentUserId)
        {
            return Unauthorized();
        }
        
        _convRepo.AcceptRequest(conversation, userId);
        return Ok(new { ConversationName = conversation.Name, Username = username});
    }

    [HttpGet]
    [Route("conversation/get/requests/{conversationId}")]
    [Authorize]
    public IActionResult GetConversationRequests(int conversationId)
    {
        var userId = _userManager.GetUserId(User);
        var conversation = _convRepo.GetConversationById(conversationId);
        if (conversation == null)
        {
            return NotFound();
        }
        if (userId == null || userId != conversation.ModeratorId)
        {
            return Unauthorized();
        }

        var usernameList = _convRepo.GetRequestsForConversation(conversation);
        return Ok(usernameList);
    } 
}