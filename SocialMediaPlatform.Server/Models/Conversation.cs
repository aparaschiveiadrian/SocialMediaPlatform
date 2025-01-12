namespace SocialMediaPlatform.Server.Models;

public class Conversation // e ceva gen grup, tot ce e legat de grup fac aici, dar o sa am si DM-urile
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? ModeratorId { get; set; } = null;
    public DateTime? LastMessageSentAt { get; set; } = DateTime.UtcNow;
    
    public List<string?> PendingUserIds { get; set; } = new List<string>();

    public List<string?> SeenUserIds { get; set; } = new List<string>();


    public virtual ICollection<UserConversation>? UserConversations { get; set; }
    public virtual ICollection<Message>? Messages { get; set; }
}