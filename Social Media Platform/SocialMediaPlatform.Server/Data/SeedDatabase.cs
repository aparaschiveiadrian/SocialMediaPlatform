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
        context.Users.AddRange(
            new ApplicationUser
            {
                UserName ="catalinmaruta",
                FirstName = "catalin",
                LastName = "maruta",
                Email ="catalinmaruta@gmail.com",
                 PasswordHash = "AQAAAAIAAYagAAAAEIH+Kcxg/qOA0mgJgrVb3NnEfoUaL5kvk3F/a7zXjr6WwaX6aYVYE8Tpkfeg+5AguA=="
            }
            );
        context.SaveChanges();
        context.Posts.AddRange(
            new Post
            {
                UserId = context.Users.FirstOrDefault(u=> u.UserName == "catalinmaruta").Id,
                ContentType = "text",
                Content = "Hello world!",
                MediaUrl = "",
                CreatedAt = DateTime.UtcNow
                
            },
            new Post
            {
                UserId = context.Users.FirstOrDefault(u=> u.UserName == "catalinmaruta").Id,
                ContentType = "text",
                Content = "AS;KDKWQEO;IQWELKASKL;DJASDK.ZXNMC,.ZDJASO;EJQWL;EQWE.,QWMD.A,SDASDAKS;DJASLKDXJZCLKZXDJKLASBeautiful sunset!",
                MediaUrl = "",
                CreatedAt = DateTime.UtcNow
            }
        );

        context.SaveChanges();
    }
}