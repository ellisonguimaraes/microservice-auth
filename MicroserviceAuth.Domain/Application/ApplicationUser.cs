using MicroserviceAuth.Domain.Identity;

namespace MicroserviceAuth.Domain.Application;

public class ApplicationUser : BaseEntity
{
    // Relationship
    public string UserId { get; set; }
    public virtual User User { get; set; }

    public Guid ApplicationId { get; set; }
    public virtual Application Application { get; set; }
}
