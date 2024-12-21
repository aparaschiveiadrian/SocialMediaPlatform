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
    
    public bool CheckFollow(string followerId, string followingId)
    {
        var existingFollow = _context.Follows
            .FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId && f.IsPending == false);
        return existingFollow != null;
    }

    public Follow? GetFollowRelation(string followerId, string followingId)
    {
        var existingFollow = _context.Follows
            .FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);
        return existingFollow;
    }
     public Follow FollowAction(string followerId, string followingId)
     {
         var followedUser = _context.Users.FirstOrDefault(u => u.Id == followingId);
         
         if (followedUser.IsPrivate)
         {
             _context.Add(new Follow { FollowerId = followerId, FollowingId = followingId, IsPending = true});
             _context.SaveChanges();
             return new Follow { FollowerId = followerId, FollowingId = followingId, IsPending = true };
         }
         
         _context.Add(new Follow { FollowerId = followerId, FollowingId = followingId, IsPending = false}); 
         _context.SaveChanges();
         return new Follow { FollowerId = followerId, FollowingId = followingId, IsPending = false };
     }

     public IEnumerable <string?> GetFollowingByUser(string followerId)
     {
         var followingList = _context.Follows
             .Where(f => f.FollowerId == followerId)
             .Select(f => f.FollowingId)
             .ToList();
         var followingUsernames = _context.Users
             .Where(user => followingList.Contains(user.Id))
             .Select(user => user.UserName)
             .ToList();
         
         /*var followingUsers = _context.Users
             .Where(user => followingList.Contains(user.Id))
             .ToList();*/

         return followingUsernames;
     }
     public IEnumerable<string?> GetFollowerByUser(string followedId)
     {
         var followerIds = _context.Follows
             .Where(f => f.FollowingId == followedId)
             .Select(f => f.FollowerId)
             .ToList();

         var followerUsernames = _context.Users
             .Where(user => followerIds.Contains(user.Id))
             .Select(user => user.UserName)
             .ToList();

         return followerUsernames;
     }

    

     public Follow AcceptFollow(Follow followRelation)
     {
         followRelation.IsPending = false;
         _context.Follows.Update(followRelation);
         _context.SaveChanges();
         return followRelation;
     }
}