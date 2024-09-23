using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Transactions.Queries.GetAllTransactions
{
    public sealed record GetAllTransactionsQuery : IQuery<IEnumerable<TransactionDto>>;
}
