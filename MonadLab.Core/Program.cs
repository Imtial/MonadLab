using MonadLab.Core;

string? name = "Asif";

var maybeName = Option<string>.From(name);

Console.WriteLine(maybeName);
