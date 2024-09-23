using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Transactions.Commands.FailTransaction
{
    public sealed record FailTransactionCommand(Guid TransactionId) : ICommand<Guid>;
}
