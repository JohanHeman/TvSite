using Microsoft.AspNetCore.Identity;

namespace TvSite.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string DisplayName{ get; set; }
    public string? ProfileImage{ get; set; }

}