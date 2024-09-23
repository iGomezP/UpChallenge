using MediatR;
using Microsoft.AspNetCore.Mvc;
using UpBack.Api.CommandToRequest;
using UpBack.Application.Services.Roles.Commands.CreateRolecommand;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Permissions;

namespace UpBack.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleRequest request)
        {
            var permissions = request.Permissions
                .Select(p => Permission.Create(Title.Create(p.Title).Value, Scope.Create(p.Scope).Value, DateTime.UtcNow, "active"))
                .ToList();

            var command = new CreateRoleCommand(
                request.Title,
                permissions,
                request.ObjectStatus
                );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
