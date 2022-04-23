using MicroserviceAuth.Domain.Application;
using Microsoft.AspNetCore.Identity;

namespace MicroserviceAuth.Domain.Identity;

/// <summary>
/// IdentityUser extensions
/// </summary>
public class User : IdentityUser
{
    public string CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string EditedBy { get; set; }

    public DateTime EditedAt { get; set; }

    // Relationship
    public virtual List<ApplicationUser> ApplicationUsers { get; set; }
}