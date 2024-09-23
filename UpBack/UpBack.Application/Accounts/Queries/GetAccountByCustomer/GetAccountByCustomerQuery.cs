using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Accounts.Queries.GetAccountsByCustomer
{
    public sealed record GetAccountByCustomerQuery(Guid CustomerId) : IQuery<AccountDto>;
}
