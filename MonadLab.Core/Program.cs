using MonadLab.Core;

var id = new OneOf<int, Guid, string>.First(1);
Console.WriteLine(id);
