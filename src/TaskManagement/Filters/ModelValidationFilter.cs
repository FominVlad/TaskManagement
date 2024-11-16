using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagement.Core.Exceptions;

namespace TaskManagement.Filters;

public class ModelValidationFilter : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            string[] errorMessages = context.ModelState
                .SelectMany(kvp => kvp.Value.Errors.Select(error => new
                {
                    Property = kvp.Key,
                    AttemptedValue = kvp.Value.AttemptedValue,
                    ErrorMessage = error.ErrorMessage,
                }))
                .Select(x => $"{x.ErrorMessage} | Property: {x.Property}, Attempted Value: {x.AttemptedValue}")
                .ToArray();

            throw new ModelValidationException(string.Join(" ", errorMessages));
        }
        else
        {
            await next().ConfigureAwait(false);
        }
    }
}