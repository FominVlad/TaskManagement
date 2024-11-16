using System.Net;

namespace TaskManagement.Core.Exceptions;

public class InvalidArgumentException : BaseException
{
    public InvalidArgumentException(string message)
        : base(message)
    {
        this.StatusCode = HttpStatusCode.BadRequest;
    }
}