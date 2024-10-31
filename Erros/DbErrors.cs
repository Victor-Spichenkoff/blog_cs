namespace blog_c_.Erros;

public class GenericDbError(string msg) : Exception
{
    public new string Message = msg;

    public void ShowErrorOnConsole()
    {
        Console.WriteLine(Message);
    }
}

public class AlreadyExistsDbError(string msg, string? field = "id") : Exception
{
    public new string Message = msg;

    public string? Field = field;
}
