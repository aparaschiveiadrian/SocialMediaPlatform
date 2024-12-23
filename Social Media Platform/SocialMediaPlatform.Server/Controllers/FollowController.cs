using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMediaPlatform.Server.Migrations;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
[Route("follow")]
public class FollowController : ControllerBase
{
    private readonly FollowRepository _followRepo;
    private readonly UserManager<ApplicationUser> _userManager;

    public FollowController(FollowRepository followRepo, UserManager<ApplicationUser> userManager)
    {
        _followRepo = followRepo;
        _userManager = userManager;
    }
     [HttpPost]
     [Route("{followingId}")]
     public IActionResult Follow([FromRoute] string followingId)
     {
         var followerId = _userManager.GetUserId(User);
         if (followerId == followingId)
         {
             return BadRequest("You cannot follow yourself.");
         }
         var existingFollow = _followRepo.CheckFollow(followerId, followingId);
         if (existingFollow)
         {
             return BadRequest("You already follow this user");
         }
    

         var followRelation = _followRepo.FollowAction(followerId, followingId);
         return Ok(followRelation);
     }

     [HttpGet]
     [Route("getFollowingsByUser/{targetUserId}")]
     public IActionResult GetFollowingsByUser([FromRoute] string targetUserId)
     {
         var currentUserId = _userManager.GetUserId(User);
         var targetUser = _userManager.Users.FirstOrDefault(f => f.Id == targetUserId);
         if (targetUser == null)
         {
             return NotFound("Follower not found");
         }

         if (currentUserId == targetUserId)
         {
             return Ok(_followRepo.GetFollowingByUser(targetUserId));
         }
         if (!targetUser.IsPrivate)
         {
             return Ok(_followRepo.GetFollowingByUser(targetUserId));
         }
         else
         {
             // verific daca user(eu) il are la follow pe followerId
             var followRelation = _followRepo.CheckFollow(currentUserId, targetUserId);
             if (!followRelation)
             {
                 return BadRequest("You can't see this user's following without being a follower.");
             }
             return Ok(_followRepo.GetFollowingByUser(targetUserId));
         }
     }
    
     [HttpGet]
     [Route("getFollowersByUser/{targetUserId}")]
     public IActionResult GetFollowersByUser([FromRoute] string targetUserId)
     {
         var currentUserId = _userManager.GetUserId(User);
         var targetUser = _userManager.Users.FirstOrDefault(u => u.Id == targetUserId);
         if (targetUser == null)
         {
             return NotFound("User not found");
         }
        
         
         if (currentUserId == targetUserId)
         {
             return Ok(_followRepo.GetFollowerByUser(targetUserId));
         }

         
         if (!targetUser.IsPrivate)
         {
             return Ok(_followRepo.GetFollowerByUser(targetUserId));
         }

         
         var followRelation = _followRepo.CheckFollow(currentUserId, targetUserId);
         if (!followRelation)
         {
             return BadRequest("You can't see this user's followers without being a follower.");
         }

         return Ok(_followRepo.GetFollowerByUser(targetUserId));
     }

     
     [HttpPut]
     [Route("accept/{followerId}")]
     [Authorize]
     public IActionResult AcceptRequest([FromRoute] string followerId)
     {
         var userId = _userManager.GetUserId(User);
         if (userId == null)
         {
             return NotFound("You are not logged in.");
         }
         var followRelation = _followRepo.GetFollowRelation(followerId, userId);
         if (followRelation == null)
         {
             return BadRequest("There is no follow request.");
         }

         if (followRelation.IsPending)
         {
             _followRepo.AcceptFollow(followRelation);
             return Ok(followRelation);
         }
         else 
         {
             return BadRequest("This user already follow you.");
         }
     }

     [HttpDelete]
     [Route("delete/follower/{followerId}")]
     [Authorize]
     public IActionResult DeleteFollower([FromRoute] string followerId) // asta o sa stearga si follow request si follower
     {
         var userId = _userManager.GetUserId(User);
         if (userId == null)
         {
             return NotFound("You are not logged in.");
         }
         var followRelation = _followRepo.GetFollowRelation(followerId, userId);
         if (followRelation == null)
         {
             return BadRequest("There is no follow.");
         }
         _followRepo.DeleteFollowRelation(followRelation);
         return Ok(followRelation);
     }
     [HttpDelete]
     [Route("delete/following/{followingId}")]
     [Authorize]
     public IActionResult DeleteFollowing([FromRoute] string followingId)
     {
         var userId = _userManager.GetUserId(User);
         if (userId == null)
         {
             return NotFound("You are not logged in.");
         }
         var followRelation = _followRepo.GetFollowRelation(userId, followingId);
         if (followRelation == null)
         {
             return BadRequest("There is no follow.");
         }
         _followRepo.DeleteFollowRelation(followRelation);
         return Ok(followRelation);
     }

     [HttpGet]
     [Route("checkIfFollower/{followingId}")]
     public IActionResult CheckIfFollower([FromRoute] string followingId) // verifici daca ai la follow un cont
     {
         var userId = _userManager.GetUserId(User);
         if (userId == null)
         {
             return BadRequest("You are not logged in.");
         }

         var followRelation = _followRepo.GetFollowRelation(userId, followingId);
         if (followRelation == null || followRelation.IsPending)
         {
             return BadRequest("You don't follow this user.");
         }
         return Ok(followRelation);
     }

     [HttpGet]
     [Route("getFollowRequests")]
     [Authorize]
     public IActionResult GetFollowRequests()
     {
         var userId = _userManager.GetUserId(User);
         if (userId == null)
         {
             return NotFound("You are not logged in.");
         }

         var followRequests = _followRepo.GetFollowRequests(userId);

         return Ok(followRequests);
     }

     [HttpGet]
     [Route("acceptAllRequests")]
     [Authorize]
     public IActionResult AcceptAllRequests()
     {
         var userId = _userManager.GetUserId(User);
         if (userId == null)
         {
             return NotFound("You are not logged in.");
         }
         var followRequests = _followRepo.AcceptAllRequests(userId);
         return Ok(followRequests);
     }
}