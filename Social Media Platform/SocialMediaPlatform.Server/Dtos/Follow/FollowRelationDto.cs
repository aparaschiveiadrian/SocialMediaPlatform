namespace SocialMediaPlatform.Server.Dtos.Follow;

public class FollowRelationDto
{
    public string FollowerId { get; set; }
    public string FollowingId { get; set; }
    public bool IsPending { get; set; }
}
