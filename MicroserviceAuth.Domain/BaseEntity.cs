namespace MicroserviceAuth.Domain;

public abstract class BaseEntity
{
    public string CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string EditedBy { get; set; }

    public DateTime EditedAt { get; set; }
}
