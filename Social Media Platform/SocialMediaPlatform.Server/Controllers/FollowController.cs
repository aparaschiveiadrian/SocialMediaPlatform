using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
[Route("follow")]
public class FollowController : ControllerBase
{
    private readonly FollowRepository _followRepo;
    public FollowController(FollowRepository followRepo)
    {
        _followRepo = followRepo;
    }
    // [HttpPost]
    // [Route("follow/{followedId}/{followerId}")]
    // public IActionResult Follow([FromRoute] string followerId, [FromRoute] string followedId)
    // {
    //     if (followerId == followedId)
    //     {
    //         return BadRequest("You cannot follow yourself.");
    //     }
    //     var existingFollow = _followRepo.CheckFollow(followerId, followedId);
    //     var checkPending = _followRepo.CheckPending(followerId, followedId);
    //     if (existingFollow)
    //     {
    //         return BadRequest("You already follow this user");
    //     }
    //
    //     if (checkPending)
    //     {
    //         return BadRequest("Your request is pending");
    //     }
    //     var followRelation = _followRepo.FollowAction(followerId, followedId);
    // }
    
}