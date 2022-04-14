using System.Diagnostics;

namespace MicroserviceAuth.Domain.DTO;

public class GenericResponse
{
    public string TraceId { get; set; }

    public int StatusCode { get; set; }

    public object Data { get; set; }

    public IEnumerable<string> Errors { get; set; }

    public GenericResponse(int statusCode)
    {
        TraceId = Activity.Current?.Id;
        StatusCode = statusCode;
    }

    public GenericResponse(int statusCode, object data) : this(statusCode)
    {
        Data = data;
    }

    public GenericResponse(int statusCode, IEnumerable<string> errors) : this(statusCode)
    {
        Errors = errors;
    }
}
