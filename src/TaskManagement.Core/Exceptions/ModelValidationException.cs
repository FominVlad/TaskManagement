using System.Net;

namespace TaskManagement.Core.Exceptions;

public class ModelValidationException : BaseException
{
    public ModelValidationException(string errorMessage)
        : base(errorMessage)
    {
        this.StatusCode = HttpStatusCode.BadRequest;
    }
}