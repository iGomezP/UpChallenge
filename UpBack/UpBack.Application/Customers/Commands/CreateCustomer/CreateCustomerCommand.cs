using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Customers.Commands.CreateCustomer
{
    public record CreateCustomerCommand(
        string Name,
        string Password,
        string LastName,
        string Email,
        string PhoneNumber,
        DateOnly BirthDay,
        string Street,
        string City,
        string ZipCode,
        string Country,
        string State) : ICommand<Guid>;
}
