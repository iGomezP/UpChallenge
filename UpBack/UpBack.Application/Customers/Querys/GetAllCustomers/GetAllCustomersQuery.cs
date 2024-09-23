using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Customers.Querys.GetAllCustomers
{
    public sealed record GetAllCustomersQuery : IQuery<IEnumerable<CustomerDto>>;
}
