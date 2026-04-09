using MonadLab.Core;

var result = new Result<int, string>.Ok(4);

var option =
    result.Map(x => x.ToString())
        .ToOption()
        .Map(int.Parse);

Console.WriteLine(option);
