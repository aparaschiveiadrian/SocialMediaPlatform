namespace SocialMediaPlatform.Server.Models;

public class UserConversation
{
    public string? UserId { get; set; }
    public int? ConversationId { get; set; }
    public virtual Conversation? Conversation { get; set; }
    public virtual ApplicationUser? User { get; set; }
}