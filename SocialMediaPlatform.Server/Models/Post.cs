using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaPlatform.Server.Models;

public class Post
{
    [Key] public int Id { get; set; }
    public string ContentType { get; set; } = string.Empty; // text sau photo sau video
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Content { get; set; } = string.Empty;
    public string? MediaUrl { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; } 
    
    public virtual ICollection<Comment> Comments { get; set; }
}