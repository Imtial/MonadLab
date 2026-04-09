namespace MonadLab.Core;

public abstract record Option<T>
{
    private Option() {}

    public sealed record None : Option<T>;
    public sealed record Some(T Value) : Option<T>;

    public static Option<T> From(T? maybeValue) => maybeValue switch
    {
        null => new Option<T>.None(),
        T value => new Option<T>.Some(value)
    };
}

public static class OptionExtensions
{
    public static bool IsSome<T>(this Option<T> option) => option switch
    {
        Option<T>.Some _ => true,
        _ => false
    };

    public static bool IsNone<T>(this Option<T> option) => option switch
    {
        Option<T>.None _ => true,
        _ => false
    };

    public static T DefaultValue<T>(this Option<T> option, T defaultValue) => option switch
    {
        Option<T>.Some some => some.Value,
        _ => defaultValue
    };

    public static T DefaultWith<T>(this Option<T> option, Func<T> fn) => option switch
    {
        Option<T>.Some some => some.Value,
        _ => fn()
    };

    public static T? GetOrDefault<T>(this Option<T> option) => option switch
    {
        Option<T>.Some some => some.Value,
        _ => default
    };

    public static T GetOrFail<T>(this Option<T> option, Exception throwIfNone) => option switch
    {
        Option<T>.Some some => some.Value,
        _ => throw throwIfNone
    };

    public static Option<U> Map<T, U>(this Option<T> option, Func<T, U> mapper) => option switch
    {
        Option<T>.Some some => new Option<U>.Some(mapper(some.Value)),
        _ => new Option<U>.None()
    };

    public static Option<U> Bind<T, U>(this Option<T> option, Func<T, Option<U>> binder) => option switch
    {
        Option<T>.Some some => binder(some.Value),
        _ => new Option<U>.None()
    };
}