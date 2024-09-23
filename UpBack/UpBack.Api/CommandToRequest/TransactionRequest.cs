using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions;

namespace UpBack.Api.CommandToRequest
{
    public sealed record TransactionRequest(
        Guid AccountId,
        TransactionType TransactionType,
        decimal Quantity,
        DateTime TransactionDate,
        string Reference,
        TransactionStatusEnum Status
        );
}
