using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Dtos.Post;
using SocialMediaPlatform.Server.Mappers;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly PostRepository _postRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    

    public PostController(PostRepository postRepo, UserManager<ApplicationUser> userManager)
    {
        _postRepo = postRepo;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("post/{postId}")]
    public IActionResult GetPost([FromRoute] int postId)
    {
        var post = _postRepo.GetPostById(postId);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpGet]
    [Route("posts")]
    public IActionResult GetPosts()
    {
        var posts = _postRepo.GetAllPosts();
        return Ok(posts);
    }

    [HttpPost]
    [Route("post/create")]
    [Authorize]
    public IActionResult CreatePost([FromBody] CreatePostDto postDto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized(userId);
        }
        var post = postDto.ToPostFromCreateDto();
        post.UserId = userId;
        _postRepo.CreatePost(post);
        return Ok(post);
    }
}