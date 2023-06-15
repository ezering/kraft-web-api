using Application.CQRS.Users.Common;
using Application.CQRS.Users.Queries.GetUser;
using Domain.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V1;
 

[Route("users")]
public sealed class UsersController : ApiController
{
    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        
        return result.Match(
            userResponse => Ok(userResponse),
            errors => Problem(errors));
    }
}