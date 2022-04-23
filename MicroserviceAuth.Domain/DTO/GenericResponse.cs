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


/// <summary>
/// Response body model
/// </summary>
public class GenericResponse
{
    public GenericResponse(int statusCode, List<string>? errors = null)
    {
        TraceId = Activity.Current?.Id;
        StatusCode = statusCode;
        Data = null;
        Errors = errors ?? new List<string>();
    }

    public GenericResponse(int statusCode, string? error = null)
    {
        TraceId = Activity.Current?.Id;
        StatusCode = statusCode;
        Data = null;
        Errors = error is not null ? new List<string> { error } : new List<string>();
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
    /// <example>null</example>
    public object? Data { get; set; }

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
