using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Repository;

public class ConversationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Conversation CreateConversation(Conversation conversation)
    {
        _context.Conversations.Add(conversation);
        _context.UserConversations.Add(new UserConversation()
        {
            Conversation = conversation,
            UserId = conversation.ModeratorId
        });
        _context.SaveChanges();
        return conversation;
    }

    public Conversation? GetConversationById(int conversationId)
    {
        var conversation = _context.Conversations.FirstOrDefault(c => c.Id == conversationId);
        return conversation;
    }

    public Conversation JoinConversation(Conversation conversation, string userId)
    {
        conversation.PendingUserIds.Add(userId);
        _context.SaveChanges();
        return conversation;
    }

    public string AcceptRequest(Conversation conversation, string userId)
    {
        conversation.PendingUserIds.Remove(userId);
        _context.UserConversations.Add(new UserConversation()
        {
            Conversation = conversation,
            UserId = userId
        });
        _context.SaveChanges();
        return conversation.Name;
    }

    public string DeclineRequest(Conversation conversation, string userId)
    {
        conversation.PendingUserIds.Remove(userId);
        _context.SaveChanges();
        return conversation.Name;
    }


    public IEnumerable<string> GetRequestsForConversation(Conversation conversation)
    {
        var usernameList = new List<string>();
        var pendingIds = conversation.PendingUserIds;
        foreach (var pendingId in pendingIds)
        {
            var username = _context.Users.FirstOrDefault(u => u.Id == pendingId).UserName;
            usernameList.Add(username);
        }

        return usernameList;
    }

    public bool CheckIfInConversation(int conversationId, string userId)
    {
        return _context.UserConversations.Any(uc => uc.ConversationId == conversationId && uc.UserId == userId);
    }

    public bool AddUserToSeenList(Conversation conversation, string userId)
    {
        if (!conversation.SeenUserIds.Contains(userId))
        {
            conversation.SeenUserIds.Add(userId);
            _context.SaveChanges();
        }

        return true;
    }

    public IEnumerable<ApplicationUser> GetConversationMembers(int conversationId)
    {
        return _context.UserConversations
            .Where(uc => uc.ConversationId == conversationId)
            .Select(uc => uc.User)
            .ToList();
    }
    
    public void RemoveUserFromConversation(Conversation conversation, string userId)
    {
        var userConversation = _context.UserConversations
            .FirstOrDefault(uc => uc.ConversationId == conversation.Id && uc.UserId == userId);

        if (userConversation != null)
        {
            _context.UserConversations.Remove(userConversation);
            _context.SaveChanges();
        }
    }
    
    public void DeleteConversation(Conversation conversation)
    {
        var messages = _context.Messages.Where(m => m.ConversationId == conversation.Id).ToList();
        _context.Messages.RemoveRange(messages);

        var userConversations = _context.UserConversations.Where(uc => uc.ConversationId == conversation.Id).ToList();
        _context.UserConversations.RemoveRange(userConversations);

        _context.Conversations.Remove(conversation);

        _context.SaveChanges();
    }
    
    public IEnumerable<Conversation> GetUserGroups(string userId)
    {
        return _context.Conversations
            .Where(c => c.UserConversations.Any(uc => uc.UserId == userId))
            .ToList();
    }
}