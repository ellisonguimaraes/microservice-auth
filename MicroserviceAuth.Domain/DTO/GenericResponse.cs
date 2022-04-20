using System.Diagnostics;

namespace MicroserviceAuth.Domain.DTO;

/// <summary>
/// Generic response body model
/// </summary>
public class GenericResponse<T>
{
    public GenericResponse(int statusCode, T? item, List<string>? errors = null)
    {
        TraceId = Activity.Current?.Id;
        StatusCode = statusCode;
        Data = item;
        Errors = errors ?? new List<string>();
    }

    /// <summary>
    /// Trace id
    /// </summary>
    /// <example>dce6aa57-64c6-4fc0-97a2-73083373d290</example>
    public string TraceId { get; set; }

    /// <summary>
    /// Status codes
    /// </summary>
    /// <example>200</example>
    public int StatusCode { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    /// <example>
    /// {
    ///     "attribute1": "value 1",
    ///     "attribute2": "value 2",
    ///     "attributeN": "value N"
    /// }
    /// </example>
    public T? Data { get; set; }

    /// <summary>
    /// Errors list
    /// </summary>
    /// <example>
    /// [
    ///     "error message 1",
    ///     "error message 2",
    ///     "error message N"
    /// ]
    ///</example>
    public IEnumerable<string> Errors { get; set; }
}
