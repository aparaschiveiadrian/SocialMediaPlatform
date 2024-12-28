namespace SocialMediaPlatform.Server.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    public int ConversationId { get; set; }
    public virtual Conversation Conversation { get; set; }
    
}