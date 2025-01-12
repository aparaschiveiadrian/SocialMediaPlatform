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
    private readonly MessageRepository _messageRepo;

    public ConversationController(UserManager<ApplicationUser> userManager, ConversationRepository convRepo,
        MessageRepository messageRepo)
    {
        _userManager = userManager;
        _convRepo = convRepo;
        _messageRepo = messageRepo;
    }

    [HttpPost]
    [Route("conversation/create/group")]
    [Authorize]
    public IActionResult CreateConversationGroup([FromBody] CreateConversationDto conversationDto)
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

        if (_convRepo.CheckIfInConversation(conversationId, userId))
        {
            return BadRequest("You are already in this conversation.");
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
        return Ok(new { ConversationName = conversation.Name, Username = username });
    }
    
    [HttpPut]
    [Route("conversation/decline/request/{username}/{conversationId}")]
    [Authorize]
    public IActionResult DeclineRequest(string username, int conversationId)
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

        _convRepo.DeclineRequest(conversation, userId);
        return Ok(new { ConversationName = conversation.Name, Username = username });
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

    [HttpGet("conversation/get/messages/{conversationId}")]
    [Authorize]
    public IActionResult GetMessages(int conversationId)
    {
        var userId = _userManager.GetUserId(User);
        var conversation = _convRepo.GetConversationById(conversationId);
        if (userId == null)
        {
            return Unauthorized();
        }

        if (conversation == null)
        {
            return NotFound();
        }

        if (!_convRepo.CheckIfInConversation(conversationId, userId))
        {
            return BadRequest("You are not part of this conversation");
        }
        
        if (!conversation.SeenUserIds.Contains(userId))
        {
            _convRepo.AddUserToSeenList(conversation, userId);
        }
        
        var messages = _messageRepo.GetMessagesForConversation(conversationId);
        return Ok(messages.Select(m => new { m.Id, m.Content, m.SentAt, m.UserId }));
    }
    [HttpGet("conversation/get/seenusernames/{conversationId}")]
    [Authorize]
    public IActionResult GetSeenUsernames(int conversationId)
    {
        var userId = _userManager.GetUserId(User);
        var conversation = _convRepo.GetConversationById(conversationId);

        if (userId == null)
        {
            return Unauthorized();
        }

        if (conversation == null)
        {
            return NotFound();
        }

        if (!_convRepo.CheckIfInConversation(conversationId, userId))
        {
            return BadRequest("You are not part of this conversation");
        }

        var seenUserIds = conversation.SeenUserIds;

        var seenUsernames = _userManager.Users
            .Where(u => seenUserIds.Contains(u.Id))
            .Select(u => u.UserName)
            .ToList();

        return Ok(seenUsernames);
    }
    
    [HttpGet("conversation/users/{conversationId}")]
    [Authorize]
    public IActionResult GetGroupUsers(int conversationId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }

        var conversation = _convRepo.GetConversationById(conversationId);
        if (conversation == null)
        {
            return NotFound("Conversation not found.");
        }

        if (!_convRepo.CheckIfInConversation(conversationId, userId))
        {
            return BadRequest("You are not part of this conversation.");
        }

        var users = _convRepo.GetConversationMembers(conversationId);

        return Ok(users.Select(u => new
        {
            u.Id,
            u.UserName,
        }));
    }

    
    [HttpPost("conversation/leave/{conversationId}")]
    [Authorize]
    public IActionResult LeaveGroup(int conversationId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }
    
        var conversation = _convRepo.GetConversationById(conversationId);
        if (conversation == null)
        {
            return NotFound("Conversation not found.");
        }
    
        if (!_convRepo.CheckIfInConversation(conversationId, userId))
        {
            return BadRequest("You are not part of this conversation.");
        }
    
        if (conversation.ModeratorId == userId)
        {
                return BadRequest("You cannot leave the group as a moderator.");
        }
    
        _convRepo.RemoveUserFromConversation(conversation, userId);
    
        return Ok(new { Message = "You have left the group." });
    }
    
    [HttpPost("conversation/kick/{conversationId}/{userId}")]
    [Authorize]
    public IActionResult KickUser(int conversationId, string userId)
    {
        var currentUserId = _userManager.GetUserId(User);
        if (currentUserId == null)
        {
            return Unauthorized();
        }

        var conversation = _convRepo.GetConversationById(conversationId);
        if (conversation == null)
        {
            return NotFound("Conversation not found.");
        }

        if (conversation.ModeratorId != currentUserId)
        {
            return Forbid("Only the moderator can kick users.");
        }

        if (!_convRepo.CheckIfInConversation(conversationId, userId))
        {
            return BadRequest("The user is not part of this conversation.");
        }

        if (currentUserId == userId)
        {
            return BadRequest("You cannot kick yourself.");
        }

        _convRepo.RemoveUserFromConversation(conversation, userId);

        return Ok(new { Message = "User has been kicked from the group." });
    }
    
    [HttpDelete("conversation/delete/{conversationId}")]
    [Authorize]
    public IActionResult DeleteGroup(int conversationId)
    {
        var currentUserId = _userManager.GetUserId(User);
        if (currentUserId == null)
        {
            return Unauthorized();
        }

        var conversation = _convRepo.GetConversationById(conversationId);
        if (conversation == null)
        {
            return NotFound("Conversation not found.");
        }

        if (conversation.ModeratorId != currentUserId)
        {
            return Forbid("Only the moderator can delete the group.");
        }

        _convRepo.DeleteConversation(conversation);

        return Ok(new { Message = "Group has been deleted successfully." });
    }
    
    
    [HttpGet("user/groups")]
    [Authorize]
    public IActionResult GetUserGroups()
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }

        var groups = _convRepo.GetUserGroups(userId);

        return Ok(groups.OrderByDescending(g => g.LastMessageSentAt).Select(g => new
        {
            g.Id,
            g.Name,
            g.LastMessageSentAt,
            ModeratorId = g.ModeratorId
        }));
    }


}