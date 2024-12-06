namespace SocialMediaPlatform.Server.Dtos.Account;

public class NewUserDto
{
    public string Id { get; set; }
    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Token { get; set; }
}