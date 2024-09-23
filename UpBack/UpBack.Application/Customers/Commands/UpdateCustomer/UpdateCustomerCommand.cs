using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Customers.Commands.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(
        Guid CustomerId,
        string PhoneNumber,
        string Street,
        string City,
        string ZipCode,
        string Country,
        string State
    ) : ICommand<Guid>;
}
