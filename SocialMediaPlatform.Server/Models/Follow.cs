using SocialMediaPlatform.Server.Data;

namespace SocialMediaPlatform.Server.Models;

public class Follow
{
    public bool IsPending { get; set; } = false;
    
    public string FollowingId { get; set; }
    public ApplicationUser Following { get; set; }
    
    public string FollowerId { get; set; }
    public ApplicationUser Follower { get; set; }
}