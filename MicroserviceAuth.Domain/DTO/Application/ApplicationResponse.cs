using MicroserviceAuth.Domain.DTO.User;

namespace MicroserviceAuth.Domain.DTO.Application;

public class ApplicationResponse : BaseResponse
{
    /// <summary>
    /// Application id
    /// </summary>
    /// <example>dce6aa57-64c6-4fc0-97a2-73083373d290</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Application unique name
    /// </summary>
    /// <example>egressprogram</example>
    public string ApplicationName { get; set; }

    /// <summary>
    /// Api key identifier
    /// </summary>
    /// <example>dce6aa57-64c6-4fc0-97a2-73083373d290</example>
    public string ApiKey { get; set; }

    /// <summary>
    /// Users into application
    /// </summary>
    public List<UserResponse>? Users { get; set; }
}
