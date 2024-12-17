using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Server.Dtos.Account;

public class UserEditDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}