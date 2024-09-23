using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Transactions.Commands.CancelTransaction
{
    public sealed record CancelTransactionCommand(Guid TransactionId) : ICommand<Guid>;
}
