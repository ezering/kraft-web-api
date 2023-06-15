using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common.Http;

namespace Presentation.Controllers.V2;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender _sender;
    
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    
    
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
    
}