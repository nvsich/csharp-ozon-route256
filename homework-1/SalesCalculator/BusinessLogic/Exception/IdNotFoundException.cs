namespace ApplicationCore.Exception;

public class IdNotFoundException : System.Exception
{
    public IdNotFoundException(string message)
        : base(message)
    {
    }
}