using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Transactions.Commands.CompleteTransaction
{
    public sealed record CompleteTransactionCommand(Guid TransactionId) : ICommand<Guid>;
}
