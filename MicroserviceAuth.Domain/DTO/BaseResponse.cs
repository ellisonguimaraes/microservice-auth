namespace MicroserviceAuth.Domain.DTO;

public abstract class BaseResponse
{
    /// <summary>
    /// Who added the application
    /// </summary>
    /// <example>egressprogram</example>
    public string CreatedBy { get; set; }

    /// <summary>
    /// When you added the application
    /// </summary>
    /// <example>2022-04-16T02:33:49.491Z</example>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Who updated the application
    /// </summary>
    /// <example>egressprogram</example>
    public string EditedBy { get; set; }

    /// <summary>
    /// When you updated the application
    /// </summary>
    /// <example>2022-04-16T02:33:49.491Z</example>
    public DateTime EditedAt { get; set; }
}
