using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Accounts.Queries.GetAccountById
{
    public sealed record GetAccountByIdQuery(Guid AccountId) : IQuery<AccountDto>;
}
