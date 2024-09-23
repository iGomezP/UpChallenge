using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Transactions.Queries.GetTransactionsByAccount
{
    public sealed record GetTransactionsByAccountQuery(Guid AccountId) : IQuery<IEnumerable<TransactionDto>>;
}
