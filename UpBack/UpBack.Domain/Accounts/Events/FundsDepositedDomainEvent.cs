using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Accounts.Events
{
    public sealed record FundsDepositedDomainEvent(Guid AccountId, decimal Amount, string Reference) : IDomainEvent;
}
