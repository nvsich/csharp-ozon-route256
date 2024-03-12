namespace ConsoleApp.Model;

public class ActionResult<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public ActionResult(T data, bool success)
    {
        Data = data;
        Success = success;
    }

    public ActionResult(string errorMessage, bool success)
    {
        ErrorMessage = errorMessage;
        Success = success;
    }
}