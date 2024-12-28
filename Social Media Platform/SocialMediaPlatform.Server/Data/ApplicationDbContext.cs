using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<UserConversation> UserConversations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "User", NormalizedName = "USER" }
        };
        builder.Entity<IdentityRole>().HasData(roles);
        builder.Entity<Follow>().HasKey(f => new { f.FollowerId, f.FollowingId });
        builder.Entity<Follow>().HasOne(f => f.Follower).WithMany(u => u.Following).HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Follow>().HasOne(f => f.Following).WithMany(u => u.Followers).HasForeignKey(f => f.FollowingId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        builder.Entity<UserConversation>()
            .HasKey(uc => new { uc.UserId, uc.ConversationId });

        builder.Entity<UserConversation>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserConversations)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserConversation>()
            .HasOne(uc => uc.Conversation)
            .WithMany(c => c.UserConversations)
            .HasForeignKey(uc => uc.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure PendingUsers relationship
        builder.Entity<Conversation>()
            .HasMany(c => c.PendingUsers)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "PendingUsers",
                j => j.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Conversation>()
                    .WithMany()
                    .HasForeignKey("ConversationId")
                    .OnDelete(DeleteBehavior.Cascade));

        // Configure SeenUserList relationship
        builder.Entity<Conversation>()
            .HasMany(c => c.SeenUserList)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "SeenUsers",
                j => j.HasOne<ApplicationUser>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Conversation>()
                    .WithMany()
                    .HasForeignKey("ConversationId")
                    .OnDelete(DeleteBehavior.Cascade));


        
    }
}