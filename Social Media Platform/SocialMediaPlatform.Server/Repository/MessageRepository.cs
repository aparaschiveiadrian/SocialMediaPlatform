using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Repository;

public class MessageRepository
{
    private readonly ApplicationDbContext _context;

     public MessageRepository(ApplicationDbContext context)
     {
         _context = context;
     }

     public Message SendMessage(Message message)
     {
         _context.Messages.Add(message);
         var conversation = _context.Conversations.FirstOrDefault(c => c.Id == message.ConversationId);
         conversation.SeenUserIds.Clear();
         _context.SaveChanges();
         return message;
     }
     public IEnumerable<Message> GetMessagesForConversation(int conversationId)
     {
         var messages = _context.Messages.Where(m => m.ConversationId == conversationId).OrderByDescending(m => m.SentAt).ToList();
         return messages;
     }
}