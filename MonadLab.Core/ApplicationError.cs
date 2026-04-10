using System.Collections.Generic;
using System.Linq;

namespace MonadLab.Core;

using IErrorContext = IReadOnlyDictionary<string, object?>;
using ErrorContext  = Dictionary<string, object?>;

public abstract record ApplicationError
{
    private ApplicationError() {}

    public sealed record AppError(IAppErrorType ErrorType) : ApplicationError;

    public sealed record AppErrors(IReadOnlyList<IAppErrorType> ErrorTypes) : ApplicationError
    {
        public AppErrors(IEnumerable<IAppErrorType> errorTypes)
            : this(errorTypes.ToList())
        {
        }
    }

    public static implicit operator ApplicationError(BaseErrorType errorType) => new AppError(errorType);

    public static ApplicationError FromMany(IEnumerable<IAppErrorType> errorTypes) => new AppErrors(errorTypes);

    public static implicit operator ApplicationError(IAppErrorType[] errorTypes) => new AppErrors(errorTypes);
}

public interface IAppErrorType
{
    string Name { get; }

    string Description { get; }

    IErrorContext Context { get; }
}

public abstract record BaseErrorType(string description, IErrorContext? context = null) : IAppErrorType
{
    public string Name => GetType().Name;
    public string Description { get; } = description;
    public IErrorContext Context { get; } = context ?? new ErrorContext();
}

public static class IntergerParseErrors
{
    public sealed record StringIsEmptyOrWhitespace() : BaseErrorType(description: "String is empty or whitespace");
    public sealed record NotAnInteger() : BaseErrorType(description: "Not an integer");
}


