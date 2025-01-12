using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Dtos.Message;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]

public class MessageController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly MessageRepository _messageRepo;
    private readonly ConversationRepository _convRepo;

    public MessageController(UserManager<ApplicationUser> userManager, MessageRepository messageRepo, ConversationRepository convRepo)
    {
        _userManager = userManager;
        _messageRepo = messageRepo;
        _convRepo = convRepo;
    }

    [HttpPost("message/send")]
    [Authorize]
    public IActionResult SendMessage([FromBody] CreateMessageDto messageDto)
    {
        var userId = _userManager.GetUserId(User);
        var conversationId = messageDto.ConversationId;
        if (userId == null)
        {
            return Unauthorized(userId);
        }
        

        if (!_convRepo.CheckIfInConversation(conversationId, userId))
        {
            return BadRequest("You are not part of this conversation");
        }
        var message = new Message
        {
            Content = messageDto.Content,
            SentAt = DateTime.UtcNow,
            UserId = userId,
            ConversationId = messageDto.ConversationId
        };
        _messageRepo.SendMessage(message);
        return Ok(message.Content);
    }
}