using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Accounts.Commands.DepositAccount
{
    public sealed record DepositAccountCommand(
        Guid AccountId,
        decimal NewBalance,
        string Reference
        ) : ICommand<Guid>;
}
