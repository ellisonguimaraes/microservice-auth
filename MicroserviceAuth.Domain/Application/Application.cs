namespace MicroserviceAuth.Domain.Application;

public class Application : BaseEntity
{
    public Guid Id { get; set; }

    public string ApplicationName { get; set; }

    public string ApiKey { get; set; }

    // Relationship
    public virtual List<ApplicationUser> ApplicationUsers { get; set; }
}
