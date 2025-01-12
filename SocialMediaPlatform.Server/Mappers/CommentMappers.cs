using System.Runtime.InteropServices.JavaScript;
using SocialMediaPlatform.Server.Dtos.Comment;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Mappers;

public static class CommentMappers
{
    public static Comment ToCommentFromCreateDto(this CreateCommentDto createCommentDto, string userId)
    {
        return new Comment()
        {
            UserId = userId,
            Content = createCommentDto.Content,
            CreatedAt = DateTime.UtcNow,
            PostId = createCommentDto.PostId,
        };
    }

        
}