using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Dtos.Group;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Repository;

public class GroupRepository
{
    private readonly ApplicationDbContext _context;

    public GroupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Group AddGroup(Group group)
    {
        _context.Groups.Add(group);
        _context.SaveChanges();
        return group;
    }
}