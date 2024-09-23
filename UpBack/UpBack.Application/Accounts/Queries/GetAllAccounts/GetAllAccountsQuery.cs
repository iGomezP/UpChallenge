using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Accounts.Queries.GetAllAccounts
{
    public sealed record GetAllAccountsQuery() : IQuery<IEnumerable<AccountDto>>;
}
