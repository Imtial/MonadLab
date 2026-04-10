namespace MonadLab.Core;

public abstract record Result<T, E>
{
    private Result() {}

    public sealed record Ok(T Value) : Result<T, E>;
    public sealed record Error(E ErrValue) : Result<T, E>;

    public static Result<T, E> FromOk(T value) => new Ok(value);

    public static Result<T, E> FromError(E error) => new Error(error);

    public static implicit operator Result<T, E>(T value) => FromOk(value);

    public static implicit operator Result<T, E>(E error) => FromError(error);
}

public static class ResultExtensions
{
    public static bool IsOk<T, E>(this Result<T, E> result) => result switch
    {
        Result<T, E>.Ok _ => true,
        _ => false
    };

    public static Option<T> ToOption<T, E>(this Result<T, E> result) => result switch
    {
        Result<T, E>.Ok ok => ok.Value,
        _ => new Option<T>.None()
    };

    public static Result<T2, E> Map<T1, T2, E>(this Result<T1, E> result, Func<T1, T2> mapper) => result switch
    {
        Result<T1, E>.Ok ok => new Result<T2, E>.Ok(mapper(ok.Value)),
        Result<T1, E>.Error error => new Result<T2, E>.Error(error.ErrValue),
        _ => throw new System.Diagnostics.UnreachableException() 
    };

    public static Result<T, E2> MapError<T, E1, E2>(this Result<T, E1> result, Func<E1, E2> mapper) => result switch
    {
        Result<T, E1>.Ok ok => new Result<T, E2>.Ok(ok.Value),
        Result<T, E1>.Error error => new Result<T, E2>.Error(mapper(error.ErrValue)),
        _ => throw new System.Diagnostics.UnreachableException() 
    };

    public static Result<T2, E> Bind<T1, T2, E>(this Result<T1, E> result, Func<T1, Result<T2, E>> binder) => result switch
    {
        Result<T1, E>.Ok ok => binder(ok.Value),
        Result<T1, E>.Error error => new Result<T2, E>.Error(error.ErrValue),
        _ => throw new System.Diagnostics.UnreachableException() 
    };

    public static T DefaultValue<T, E>(this Result<T, E> result, T defaultValue) => result switch
    {
        Result<T, E>.Ok ok => ok.Value,
        _ => defaultValue
    };

    public static T DefaultWith<T, E>(this Result<T, E> result, Func<T> fn) => result switch
    {
        Result<T, E>.Ok ok => ok.Value,
        _ => fn()
    };

    public static T? GetOrDefault<T, E>(this Result<T, E> result) => result switch
    {
        Result<T, E>.Ok ok => ok.Value,
        _ => default
    };
}