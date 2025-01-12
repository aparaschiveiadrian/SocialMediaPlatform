namespace SocialMediaPlatform.Server.Dtos.Comment;

public class GetCommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; }
    public bool IsEdited { get; set; }
}