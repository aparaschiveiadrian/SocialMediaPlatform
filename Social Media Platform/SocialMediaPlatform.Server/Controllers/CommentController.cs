using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Server.Dtos.Comment;
using SocialMediaPlatform.Server.Dtos.Post;
using SocialMediaPlatform.Server.Mappers;
using SocialMediaPlatform.Server.Models;
using SocialMediaPlatform.Server.Repository;

namespace SocialMediaPlatform.Server.Controllers;
[ApiController]
[Route("comment")]
public class CommentController  : ControllerBase
{
    private readonly CommentRepository _commRepo;
    private readonly UserManager<ApplicationUser> _userManager;

    public CommentController(CommentRepository commRepo, UserManager<ApplicationUser> userManager, PostRepository postRepo)
    {
        _commRepo = commRepo;
        _userManager = userManager;
    }
    [HttpPost]
    [Route("create")]
    [Authorize]
    public IActionResult CreateComment([FromBody] CreateCommentDto commentDto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Unauthorized(userId);
        }
        var comment = commentDto.ToCommentFromCreateDto(userId);
        _commRepo.CreateComment(comment);
        return Ok(comment);
    }

    [HttpGet]
    [Route("get/{postId}")]
    public IActionResult GetComment([FromRoute] int postId)
    {
        var commentList = _commRepo.GetCommentsByPost(postId);
        if (commentList == null || !commentList.Any())
        {
            //return NotFound();
            return NoContent();
        }
        return Ok(commentList);
    }

    [HttpDelete]
    [Route("delete/{commentId}")]
    [Authorize]
    public IActionResult DeleteComment([FromRoute] int commentId)
    {
        var userId  = _userManager.GetUserId(User);
        var comment = _commRepo.GetCommentById(commentId);
        
        var user = _userManager.Users.FirstOrDefault(u=>u.Id==userId);
        var isAdmin = _userManager .IsInRoleAsync(user, "Admin").Result;
        
        if (comment == null)
        {
            return NotFound();
        }

        if (comment.UserId != userId && !isAdmin)
        {
            return Unauthorized("You are not authorized to delete this comment.");
        }
        _commRepo.DeleteComment(comment);
        return Ok(comment);
    }
    [HttpPut]
    [Route("edit/{commentId}")]
    [Authorize]
    public IActionResult EditComment([FromRoute] int commentId, [FromBody] EditCommentDto commentDto)
    {
        var userId  = _userManager.GetUserId(User);
        var comment = _commRepo.GetCommentById(commentId);
        
        var user = _userManager.Users.FirstOrDefault(u=>u.Id==userId);
        var isAdmin = _userManager .IsInRoleAsync(user, "Admin").Result;
        if (comment == null)
        {
            return NotFound();
        }

        if (comment.UserId != userId && !isAdmin)
        {
            return Unauthorized("You are not authorized to edit this comment.");
        }
        var editedComment = _commRepo.EditComment(comment, commentDto);
        return Ok(editedComment);
    }
}