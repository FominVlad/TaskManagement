using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Filters;

namespace TaskManagement.Controllers;

[Authorize]
[ModelValidationFilter]
[ServiceFilter(typeof(ExceptionFilter))]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
}