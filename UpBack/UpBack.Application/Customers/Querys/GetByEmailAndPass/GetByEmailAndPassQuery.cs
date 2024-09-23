using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Customers.Querys.GetByEmailAndPass
{
    public sealed record GetByEmailAndPassQuery(string Email, string Password) : IQuery<CustomerDto>;
}
