namespace TaskManagement.Models;

public class ExceptionResponse
{
    public int StatusCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? AdditionalInfo { get; set; }
}