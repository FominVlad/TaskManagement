using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.Core.Exceptions;
using TaskManagement.Models;

namespace TaskManagement.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    #region Private Fields

    private readonly IConfiguration configuration;
    private readonly ILogger<ExceptionFilter> logger;

    #endregion

    #region Public Constructors

    public ExceptionFilter(
        IConfiguration configuration,
        ILogger<ExceptionFilter> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    #endregion

    #region Public Methods

    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        BaseException exception = context.Exception as BaseException;

        context.HttpContext.Response.StatusCode = (int)(exception?.StatusCode ?? HttpStatusCode.InternalServerError);
        context.HttpContext.Response.ContentType = "application/json";

        await context.HttpContext.Response
            .WriteAsJsonAsync(new ExceptionResponse
            {
                StatusCode = context.HttpContext.Response.StatusCode,
                ErrorMessage = exception?.Message ?? this.GetDefaultErrorMessage(this.configuration),
                AdditionalInfo = exception?.AdditionalInfo,
            });

        context.ExceptionHandled = true;
    }

    #endregion

    #region Private Methods

    private string GetDefaultErrorMessage(IConfiguration configuration)
    {
        return configuration.GetValue<string>("Exceptions:DefaultErrorMessage");
    }

    #endregion

}