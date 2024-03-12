namespace ApplicationCore.Exception;

public class InvalidInputException : System.Exception
{
    public InvalidInputException(string message)
        : base(message)
    {
    }
}