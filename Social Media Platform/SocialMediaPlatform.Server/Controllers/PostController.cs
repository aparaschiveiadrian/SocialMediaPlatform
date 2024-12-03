using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly PostRepository _postRepo;

    public PostController(PostRepository postRepo)
    {
        _postRepo = postRepo;
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
}