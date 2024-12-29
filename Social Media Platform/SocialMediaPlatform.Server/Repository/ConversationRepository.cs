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

    public Conversation GetConversationById(int conversationId)
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
}