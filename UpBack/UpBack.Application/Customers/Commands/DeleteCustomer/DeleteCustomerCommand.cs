using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Customers.Commands.DeleteCustomer
{
    public sealed record DeleteCustomerCommand(Guid CustomerId) : ICommand<Guid>;
}
