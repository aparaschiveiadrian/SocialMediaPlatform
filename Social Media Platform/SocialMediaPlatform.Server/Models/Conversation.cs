namespace SocialMediaPlatform.Server.Models;

public class Conversation
{
    public int Id { get; set; }
    public string? ModeratorId { get; set; } = null;
    public DateTime LastMessageSentAt { get; set; }

    // Direct collections of ApplicationUser
    public virtual ICollection<ApplicationUser> PendingUsers { get; set; } = new List<ApplicationUser>();
    public virtual ICollection<ApplicationUser> SeenUserList { get; set; } = new List<ApplicationUser>();

    public virtual ICollection<UserConversation>? UserConversations { get; set; }
    public virtual ICollection<Message>? Messages { get; set; }
}