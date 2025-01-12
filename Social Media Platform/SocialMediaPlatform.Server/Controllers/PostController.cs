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
    [Route("posts/{userName}")]
    public IActionResult GetPostsByUser([FromRoute] string userName)
    {
        var posts = _postRepo.GetPostsByUsername(userName);
        if (!posts.Any())
        {
            return NotFound();
        }

        return Ok(posts);
    }

    [HttpGet]
    [Route("posts")]
    public IActionResult GetPosts()
    {
        var posts = _postRepo.GetAllPostsWithUsers();
        if (!posts.Any())
        {
            return NotFound();
        }

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

        var post = postDto.ToPostFromCreateDto(userId);
        _postRepo.CreatePost(post);
        return Ok(post);
    }

    [HttpDelete]
    [Route("post/delete/{postId}")]
    [Authorize]
    public IActionResult DeletePost([FromRoute] int postId)
    {
        var userId = _userManager.GetUserId(User);
        var post = _postRepo.GetPostById(postId);
        if (post == null)
        {
            return NotFound();
        }
        var user =  _userManager.Users.FirstOrDefault(u => u.Id == userId);
        var isAdmin = _userManager.IsInRoleAsync(user, "Admin").Result; 
        
        if (post.UserId != userId && !isAdmin)
        {
            return Unauthorized(userId);
        }
        
        _postRepo.DeletePost(post);
        return Ok(post);
    }

}