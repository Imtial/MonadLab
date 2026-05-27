namespace MonadLab.Core;

public abstract record OneOf<T1, T2>
{
    private OneOf() { }

    public sealed record First(T1 Data) : OneOf<T1, T2>;
    public sealed record Second(T2 Data) : OneOf<T1, T2>;
}

public abstract record OneOf<T1, T2, T3>
{
    private OneOf() { }

    public sealed record First(T1 Data) : OneOf<T1, T2, T3>;
    public sealed record Second(T2 Data) : OneOf<T1, T2, T3>;
    public sealed record Third(T3 Data) : OneOf<T1, T2, T3>;
}

public abstract record OneOf<T1, T2, T3, T4>
{
    private OneOf() { }

    public sealed record First(T1 Data) : OneOf<T1, T2, T3, T4>;
    public sealed record Second(T2 Data) : OneOf<T1, T2, T3, T4>;
    public sealed record Third(T3 Data) : OneOf<T1, T2, T3, T4>;
    public sealed record Fourth(T4 Data) : OneOf<T1, T2, T3, T4>;
}

public abstract record OneOf<T1, T2, T3, T4, T5>
{
    private OneOf() { }

    public sealed record First(T1 Data) : OneOf<T1, T2, T3, T4, T5>;
    public sealed record Second(T2 Data) : OneOf<T1, T2, T3, T4, T5>;
    public sealed record Third(T3 Data) : OneOf<T1, T2, T3, T4, T5>;
    public sealed record Fourth(T4 Data) : OneOf<T1, T2, T3, T4, T5>;
    public sealed record Fifth(T5 Data) : OneOf<T1, T2, T3, T4, T5>;
}