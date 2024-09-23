using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Accounts.Events
{
    public sealed record FundsTransferredDomainEvent(Guid SourceAccountId, Guid TargetAccountId, decimal Amount, string Reference) : IDomainEvent;

}
