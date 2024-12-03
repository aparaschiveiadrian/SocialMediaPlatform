using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) :base(options)
    {
        
    }
    public DbSet<Post> Posts { get; set; }
}