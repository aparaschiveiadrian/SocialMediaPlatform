namespace SocialMediaPlatform.Server.Dtos.Message;

public class CreateMessageDto
{
    public string Content { get; set; }
    public int ConversationId { get; set; }
}