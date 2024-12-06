namespace SocialMediaPlatform.Server.Dtos.Post;

public class CreatePostDto
{
    public string ContentType { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
    public string? MediaUrl { get; set; }
}