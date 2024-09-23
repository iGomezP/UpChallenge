using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Accounts.Commands.TransferToAccount
{
    public sealed record TransferToAccountCommand(
        Guid SourceAccountId,
        Guid TargetAccountId,
        decimal Amount,
        string Reference
    ) : ICommand<Guid>;
}
