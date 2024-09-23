using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Accounts.Queries.GetByNumber
{
    public sealed record GetByNumberQuery(string accountNumber) : IQuery<AccountDto>;
}
