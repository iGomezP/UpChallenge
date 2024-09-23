using MediatR;
using Microsoft.AspNetCore.Mvc;
using UpBack.Api.CommandToRequest;
using UpBack.Application.Services;
using UpBack.Application.Transactions.Commands.CancelTransaction;
using UpBack.Application.Transactions.Commands.CompleteTransaction;
using UpBack.Application.Transactions.Commands.CreateTransaction;
using UpBack.Application.Transactions.Commands.FailTransaction;
using UpBack.Application.Transactions.Commands.RefundTransaction;
using UpBack.Application.Transactions.Commands.RejectTransaction;
using UpBack.Application.Transactions.Commands.UpdateTransaction;
using UpBack.Application.Transactions.Queries.GetAllTransactions;
using UpBack.Application.Transactions.Queries.GetTransactionById;
using UpBack.Application.Transactions.Queries.GetTransactionsByAccount;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Api.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController(IMediator mediator, IGuidValidationService guidValidationService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IGuidValidationService _guidValidationService = guidValidationService;

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] TransactionRequest request)
        {
            var command = new CreateTransactionCommand
            (
                request.AccountId,
                request.TransactionType,
                request.Quantity,
                request.TransactionDate,
                request.Reference,
                request.Status
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionAsync(string id, [FromBody] TransactionRequest request)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new UpdateTransactionCommand
            (
                validId,
                request.Quantity,
                request.Reference,
                request.Status
            );

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTransactionAsync(string id)
        //{
        //    var command = new DeleteTransactionCommand(validId);

        //    var result = await _mediator.Send(command);

        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }

        //    return NoContent();
        //}

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteTransactionAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new CompleteTransactionCommand(validId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelTransactionAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new CancelTransactionCommand(validId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("{id}/fail")]
        public async Task<IActionResult> FailTransactionAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new FailTransactionCommand(validId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("{id}/refund")]
        public async Task<IActionResult> RefundTransactionAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new RefundTransactionCommand(validId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectTransactionAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new RejectTransactionCommand(validId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionByIdAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var query = new GetTransactionByIdQuery(validId);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("account/{accountId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsByAccountAsync(string accountId)
        {
            var validId = _guidValidationService.ValidateGuid(accountId);
            var query = new GetTransactionsByAccountQuery(validId);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsAsync()
        {
            var query = new GetAllTransactionsQuery();
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
