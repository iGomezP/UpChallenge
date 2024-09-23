using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Transactions;

namespace UpBack.Application.Transactions.Commands.UpdateTransaction
{
    public sealed record UpdateTransactionCommand(
        Guid TransactionId,
        decimal NewQuantity,
        string NewReference,
        TransactionStatusEnum NewStatus
    ) : ICommand<Guid>;
}
