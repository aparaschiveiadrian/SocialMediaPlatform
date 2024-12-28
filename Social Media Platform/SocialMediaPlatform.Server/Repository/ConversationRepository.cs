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
}