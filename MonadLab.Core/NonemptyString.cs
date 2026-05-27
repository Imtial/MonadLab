namespace MonadLab.Core;

public interface IExactSize { abstract int ExactSize { get; } }
public interface IMinSize { abstract int MinSize { get; } }
public interface IMaxSize { abstract int MaxSize { get; } }

public static class ConstSize
{
    public readonly struct Exact2 : IExactSize { public int ExactSize => 2; } 
    public readonly struct Min4Max8 : IMinSize, IMaxSize
    {
        public int MinSize => 4;
        public int MaxSize => 8;
    }
}

public abstract class NonemptyString<T> where T: IExactSize, IMinSize, IMaxSize
{
    private NonemptyString() { }

    private static bool IsValid(string? s, T marker)
    {
        if (s is null)
        {
            return false;
        }

        var trimmedView = s.AsSpan().Trim();

        if (marker is IExactSize exactSize)
        {
            return trimmedView.Length == exactSize.ExactSize;
        }

        if (marker is IMinSize minSize && trimmedView.Length < minSize.MinSize)
        {
            return false;
        }

        if (marker is IMaxSize maxSize && trimmedView.Length > maxSize.MaxSize)
        {
            return false;
        }

        return true;
    }
}