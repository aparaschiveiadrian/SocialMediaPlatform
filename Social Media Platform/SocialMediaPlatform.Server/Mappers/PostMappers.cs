using SocialMediaPlatform.Server.Dtos.Post;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Mappers;

public static class PostMappers
{
    public static Post ToPostFromCreateDto(this CreatePostDto createPostDto)
    {
        return new Post()
        {
            UserId = "a",
            ContentType = createPostDto.ContentType,
            Content = createPostDto.Content,
            CreatedAt = createPostDto.CreatedAt,
            MediaUrl = createPostDto.MediaUrl
        };
    }
}