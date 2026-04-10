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

    public static implicit operator Option<T>(T? maybeValue) => From(maybeValue);
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

    public static T? Get<T>(this Option<T> option) => option switch
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
        Option<T>.Some some => mapper(some.Value),
        _ => new Option<U>.None()
    };

    public static Option<U> Map2<T1, T2, U>(this (Option<T1> Option1, Option<T2> Option2) optionPair, Func<T1, T2, U> mapper) => optionPair switch
    {
        (Option<T1>.Some some1, Option<T2>.Some some2) => mapper(some1.Value, some2.Value),
        _ => new Option<U>.None()
    };

    public static Option<U> Bind<T, U>(this Option<T> option, Func<T, Option<U>> binder) => option switch
    {
        Option<T>.Some some => binder(some.Value),
        _ => new Option<U>.None()
    };

    public static List<T> ToList<T>(this Option<T> option) => option switch
    {
        Option<T>.Some some => [ some.Value ],
        _ => []
    };

    public static Option<T> OrElse<T>(this Option<T> option, Option<T> ifNone) => option switch
    {
        Option<T>.None _ => ifNone,
        _ => option
    };

    public static Option<T> OrElseWith<T>(this Option<T> option, Func<Option<T>> fnIfNone) => option switch
    {
        Option<T>.None _ => fnIfNone(),
        _ => option
    };

    public static Option<T> Flatten<T>(this Option<Option<T>> maybeOption) => maybeOption switch
    {
        Option<Option<T>>.Some someOption when someOption.IsSome() => someOption.Value,
        _ => new Option<T>.None()
    };

    public static bool Contains<T>(this Option<T> option, T query) => option switch
    {
        Option<T>.Some some => some.Value!.Equals(query),
        _ => false
    };

    public static bool Contains<T>(this Option<T> option, Func<T, bool> predicate) => option switch
    {
        Option<T>.Some some => predicate(some.Value),
        _ => false
    };
}