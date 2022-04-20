namespace FluentResults;

public class ResultExtensions
{
    public static Result Fail(IEnumerable<IError> errors)
    {
        Result result = new Result();
        result.WithErrors(errors);
        return result;
    }

    public static Result<TValue> Fail<TValue>(IEnumerable<IError> errors)
    {
        Result<TValue> result = new Result<TValue>();
        result.WithErrors(errors);
        return result;
    }
}
