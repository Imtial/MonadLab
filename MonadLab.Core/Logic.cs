namespace MonadLab.Core;


public static class Utils
{
    public static ApplicationResult<int> Parse(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return new IntergerParseErrors.StringIsEmptyOrWhitespace();
        }

        if (int.TryParse(s, out var number))
        {
            return number;
        }

        return new IntergerParseErrors.NotAnInteger();
    }
}