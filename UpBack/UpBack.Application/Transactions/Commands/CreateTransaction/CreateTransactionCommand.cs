using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions;

namespace UpBack.Application.Transactions.Commands.CreateTransaction
{
    public sealed record CreateTransactionCommand(
        Guid AccountId,
        TransactionType Type,
        decimal Quantity,
        DateTime TransactionDate,
        string Reference,
        TransactionStatusEnum Status
    ) : ICommand<Guid>;
}
