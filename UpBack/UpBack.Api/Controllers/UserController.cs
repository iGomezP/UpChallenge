using MediatR;
using Microsoft.AspNetCore.Mvc;
using UpBack.Api.CommandToRequest;
using UpBack.Application.Customers.Commands.CreateCustomer;
using UpBack.Application.Customers.Querys.GetByEmailAndPass;

using UpBack.Application.Services;
using UpBack.Application.Services.Roles.Querys.GetById;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public UserController(IAuthenticationService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CustomerRequest request)
        {
            var birthDayDto = new BirthDayDto
            {
                Year = request.BirthDay.Year,
                Month = request.BirthDay.Month,
                Day = request.BirthDay.Day
            };

            var command = new CreateCustomerCommand
            (
                request.Name,
                request.Password,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                BirthDayDto.ToDateOnly(birthDayDto),
                request.Street,
                request.City,
                request.ZipCode,
                request.Country,
                request.State
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest loginRequest)
        {
            var query = new GetByEmailAndPassQuery(loginRequest.Email, loginRequest.Password);
            var result = await _mediator.Send(query);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            var roleQuery = new GetRoleByIdQuery(result.Value.RoleId);
            var roleResult = await _mediator.Send(roleQuery);

            if (result.IsFailure)
            {
                return BadRequest(roleResult.Error);
            }

            var token = _authenticationService.GenerateAccessToken(result.Value, roleResult.Value);

            return Ok(new
            {
                customerId = result.Value.Id,
                token
            });
        }
    }
}
