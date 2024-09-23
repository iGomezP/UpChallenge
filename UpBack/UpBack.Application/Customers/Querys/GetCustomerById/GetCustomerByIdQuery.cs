using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Customers.Querys.GetCustomerById
{
    public sealed record GetCustomerByIdQuery(Guid CustomerId) : IQuery<CustomerDto>;
}
