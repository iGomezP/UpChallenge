using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Transactions.Commands.RefundTransaction
{
    public sealed record RefundTransactionCommand(Guid TransactionId) : ICommand<Guid>;
}
