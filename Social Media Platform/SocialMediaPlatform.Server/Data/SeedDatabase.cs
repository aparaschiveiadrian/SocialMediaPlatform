using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Models;

public static class SeedDatabase
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (context.Posts.Any()) return;

        context.Posts.AddRange(
            new Post
            {
                ContentType = "text",
                Content = "Hello world!",
                MediaUrl = "",
                CreatedAt = DateTime.UtcNow
            },
            new Post
            {
                ContentType = "text",
                Content = "AS;KDKWQEO;IQWELKASKL;DJASDK.ZXNMC,.ZDJASO;EJQWL;EQWE.,QWMD.A,SDASDAKS;DJASLKDXJZCLKZXDJKLASBeautiful sunset!",
                MediaUrl = "",
                CreatedAt = DateTime.UtcNow
            }
        );

        context.SaveChanges();
    }
}