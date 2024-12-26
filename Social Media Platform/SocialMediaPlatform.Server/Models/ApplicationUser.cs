using Microsoft.AspNetCore.Identity;

namespace SocialMediaPlatform.Server.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public bool IsPrivate { get; set; } = false;
    
    public virtual ICollection<Post> Posts { get; set; }
    
    public virtual ICollection<Comment> Comments { get; set; }
    
    public ICollection<Follow> Following { get; set; }
    public ICollection<Follow> Followers { get; set; }
    
    public virtual ICollection<Group> Groups { get; set; }
    
}