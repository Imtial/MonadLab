using MonadLab.Core;

string? name = "Asif";

var maybeName = OptionExtensions.MaybeCapitalized(name);

Console.WriteLine(maybeName);
