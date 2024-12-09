namespace SocialMediaPlatform.Server.Dtos.Post;

public class PostWithUserDto
{
    public int Id { get; set; }
    public string ContentType { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
    public string? MediaUrl { get; set; }
    public string UserId { get; set; }
    public string Username { get; set; }
}