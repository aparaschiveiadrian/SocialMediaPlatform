using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Repository;

public class FollowRepository
{
    private readonly ApplicationDbContext _context;

    public FollowRepository(ApplicationDbContext context)
    {
        _context = context;
        
    }

    public bool CheckPending(string followerId, string followingId)
    {
        var existingFollow = _context.Follows
            .FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);
        return existingFollow.isPending;
    }
    public bool CheckFollow(string followerId, string followingId)
    {
        var existingFollow = _context.Follows
            .FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);
        return existingFollow != null;
    }

    // public Follow FollowAction(string followerId, string followingId)
    // {
    //     
    // }
}