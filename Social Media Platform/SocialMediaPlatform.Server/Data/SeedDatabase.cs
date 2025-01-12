using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Models;

public static class SeedDatabase
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        SeedRoles(roleManager).Wait();
        SeedAdminUser(userManager).Wait();
        
        if (context.Posts.Any()) return; // daca sunt posts e si user-u initializat
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
    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
    }

    private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "admin@yahoo.com";
        var adminUserName = "admin";
        var adminPassword = "Admin12";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminEmail
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}