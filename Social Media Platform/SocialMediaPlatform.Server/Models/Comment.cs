using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Server.Models;

public class Comment
{
    [Key] public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsEdited { get; set; } = false;
    
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    public int PostId { get; set; }
    public virtual Post Post { get; set; }
    
}   