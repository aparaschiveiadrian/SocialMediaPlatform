using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Dtos.Comment;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Repository;

public class CommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Comment CreateComment(Comment comment)
    {
        _context.Add(comment);
        _context.SaveChanges();
        return comment;
    }

    public IEnumerable<GetCommentDto>? GetCommentsByPost(int postId)
    {
        var commentDtos = (from comment in _context.Comments
            join user in _context.Users
                on comment.UserId equals user.Id
            where comment.PostId == postId
            select new GetCommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                Username = user.UserName,
                IsEdited = comment.IsEdited
            }).OrderBy(comment => comment.CreatedAt).ToList();
        return commentDtos;
        //return _context.Comments.Where(c => c.PostId == postId);
        //var comments = _context.Comments
            // .Include(c => c.User) 
            //.Where(c => c.PostId == postId);
            //.ToList();
        //return comments;
    }

    public Comment? GetCommentById(int commentId)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);
        return comment;
    }
    public Comment? DeleteComment(Comment comment)
    {
        _context.Comments.Remove(comment);
        _context.SaveChanges();
        return comment;
    }

    public EditCommentDto? EditComment(Comment comment, EditCommentDto editDto)
    {
        comment.Content = editDto.Content;
        comment.IsEdited = true;
        _context.SaveChanges();
        return editDto;
    }
}