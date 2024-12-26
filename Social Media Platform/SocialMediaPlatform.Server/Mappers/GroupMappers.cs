using SocialMediaPlatform.Server.Dtos.Group;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Mappers;

public static class GroupMappers
{
    public static Group ToGroupFromCreateDto(this CreateGroupDto createCommentDto, string userId)
    {
        return new Group()
        {
            ModeratorId = userId,
            Name = createCommentDto.Name,
        };
    }   
}