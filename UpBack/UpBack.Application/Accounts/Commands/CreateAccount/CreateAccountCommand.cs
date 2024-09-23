using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Accounts.Commands.CreateAccount
{
    public sealed record CreateAccountCommand(
        Guid CustomerId,
        decimal InitialBalance,
        string ObjectStatus
        ) : ICommand<Guid>;
}
