namespace SocialMediaPlatform.Server.Models;

public class Group // nu cred ca e folosit
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;

    public string ModeratorId { get; set; } = string.Empty;
    
    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}