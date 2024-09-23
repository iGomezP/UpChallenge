using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Transactions.Commands.RejectTransaction
{
    public sealed record RejectTransactionCommand(Guid TransactionId) : ICommand<Guid>;
}
