using MediatR;
using Microsoft.AspNetCore.Mvc;
using UpBack.Api.CommandToRequest;
using UpBack.Application.Accounts.Commands.CreateAccount;
using UpBack.Application.Accounts.Commands.DeleteAccount;
using UpBack.Application.Accounts.Commands.DepositAccount;
using UpBack.Application.Accounts.Commands.TransferToAccount;
using UpBack.Application.Accounts.Commands.WithdrawAccount;
using UpBack.Application.Accounts.Queries.GetAccountById;
using UpBack.Application.Accounts.Queries.GetAccountsByCustomer;
using UpBack.Application.Accounts.Queries.GetByNumber;
using UpBack.Application.Services;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController(IMediator mediator, IGuidValidationService guidValidationService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IGuidValidationService _guidValidationService = guidValidationService;

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountRequest request)
        {
            var command = new CreateAccountCommand
            (
                request.CustomerId,
                request.InitialBalance,
                "active"
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("{id}/transfer")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TransferToAsync(string id, [FromBody] TransferToRequest request)
        {
            var validId = _guidValidationService.ValidateGuid(id);

            var command = new TransferToAccountCommand
            (
                validId,
                request.TargetAccountId,
                request.Amount,
                request.Reference
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpPost("{id}/withdraw")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> WithdrawAsync(string id, [FromBody] WithdrawRequest request)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new WithdrawAccountCommand
            (
                validId,
                request.Amount,
                request.Reference
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpPost("{id}/deposit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DepositAsync(string id, [FromBody] AccountRequest request)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new DepositAccountCommand
            (
                validId,
                request.InitialBalance,
                request.Reference
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAccountAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new DeleteAccountCommand(validId);
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccountByIdAsync([FromRoute] string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);

            var query = new GetAccountByIdQuery(validId);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("customer/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccountByCustomerIdAsync(string customerId)
        {
            var validId = _guidValidationService.ValidateGuid(customerId);
            var query = new GetAccountByCustomerQuery(validId);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("account-number/{accountNumber}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAccountNumberAsync(string accountNumber)
        {
            var query = new GetByNumberQuery(accountNumber);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
