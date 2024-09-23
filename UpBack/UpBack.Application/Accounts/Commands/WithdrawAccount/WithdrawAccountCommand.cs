using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Accounts.Commands.WithdrawAccount
{
    public sealed record WithdrawAccountCommand(
        Guid AccountId,
        decimal Amount,
        string Reference
    ) : ICommand<Guid>;
}
