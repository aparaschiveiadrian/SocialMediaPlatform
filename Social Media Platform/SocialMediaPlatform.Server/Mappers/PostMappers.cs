using SocialMediaPlatform.Server.Dtos.Post;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Mappers;

public static class PostMappers
{
    public static Post ToPostFromCreateDto(this CreatePostDto createPostDto, string userId)
    {
        return new Post()
        {
            UserId = userId,
            ContentType = createPostDto.ContentType,
            Content = createPostDto.Content,
            CreatedAt = createPostDto.CreatedAt,
            MediaUrl = createPostDto.MediaUrl
        };
    }
}