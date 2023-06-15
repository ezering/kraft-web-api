using Application.CQRS.Roles.Queries.GetAllRoles;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V1;

[Route("roles")]
public sealed class RolesController : ApiController
{
    
    [HttpGet("get-all-roles")]
    public async Task<IActionResult> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllRolesQuery();
        var result = await Sender.Send(query, cancellationToken);
        
        return result.Match(
            roles => Ok(roles),
            errors => Problem(errors));
    }
}