using UpBack.Domain.Abstractions;

namespace UpBack.Domain.Accounts.Events
{
    public sealed record FundsWithdrawnDomainEvent(Guid AccountId, decimal Amount, string Reference) : IDomainEvent;

}
