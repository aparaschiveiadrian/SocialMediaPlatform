namespace SocialMediaPlatform.Server.Dtos.Account;

public class UserDetailsDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    
    public bool IsPrivate { get; set; }
}