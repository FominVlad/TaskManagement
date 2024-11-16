using System.Net;

namespace TaskManagement.Core.Exceptions;

public class BaseException : Exception
{
    public BaseException(
        string errorMessage,
        string? additionalInfo = null)
        : base(errorMessage)
    {
        this.AdditionalInfo = additionalInfo;
    }

    public HttpStatusCode StatusCode { get; set; }

    public string? AdditionalInfo { get; set; }
}