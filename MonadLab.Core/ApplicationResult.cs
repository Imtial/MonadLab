namespace MonadLab.Core;

public record ApplicationResult<T>(Result<T, ApplicationError> Value)
{
    public static implicit operator ApplicationResult<T>(T value) =>
        new(Result<T, ApplicationError>.FromOk(value));

    public static implicit operator ApplicationResult<T>(ApplicationError error) =>
        new(Result<T, ApplicationError>.FromError(error));

    public static implicit operator ApplicationResult<T>(BaseErrorType errorType) =>
        new(Result<T, ApplicationError>.FromError(errorType));

    public static implicit operator ApplicationResult<T>(BaseErrorType[] errorTypes) =>
        new(Result<T, ApplicationError>.FromError(
            new ApplicationError.AppErrors(errorTypes)));

    public static implicit operator Result<T, ApplicationError>(ApplicationResult<T> result) =>
        result.Value;

    public static implicit operator ApplicationResult<T>(Result<T, ApplicationError> result) =>
        new(result);
}