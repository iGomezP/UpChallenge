using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Application.Transactions.Queries.GetTransactionById
{
    public sealed record GetTransactionByIdQuery(Guid TransactionId) : IQuery<TransactionDto>;
}
