using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpBack.Api.CommandToRequest;
using UpBack.Application.Customers.Commands.CreateCustomer;
using UpBack.Application.Customers.Commands.DeleteCustomer;
using UpBack.Application.Customers.Commands.UpdateCustomer;
using UpBack.Application.Customers.Querys.GetAllCustomers;
using UpBack.Application.Customers.Querys.GetCustomerById;
using UpBack.Application.Services;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "UpBackScheme")]
    public class CustomersController(IMediator mediator, IGuidValidationService guidValidationService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IGuidValidationService _guidValidationService = guidValidationService;

        [HttpPost]
        [AuthAttribute("CustomerManagement.Write")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerRequest request)
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

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        [AuthAttribute("CustomerManagement.Write")]
        public async Task<IActionResult> UpdateCustomerAsync(string id, [FromBody] CustomerRequest request)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new UpdateCustomerCommand
            (
                validId, // CustomerId
                request.PhoneNumber,
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

        [HttpDelete("{id}")]
        [AuthAttribute("CustomerManagement.Write")]
        public async Task<IActionResult> DeleteCustomerAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var command = new DeleteCustomerCommand(validId);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AuthAttribute("CustomerManagements.Read")]
        public async Task<IActionResult> GetCustomerByIdAsync(string id)
        {
            var validId = _guidValidationService.ValidateGuid(id);
            var query = new GetCustomerByIdQuery(validId);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [AuthAttribute("CustomerManagement.Read")]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var query = new GetAllCustomersQuery();
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
