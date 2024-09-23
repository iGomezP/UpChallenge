using UpBack.Application.Abstractions.Messaging;

namespace UpBack.Application.Accounts.Commands.DeleteAccount
{
    public sealed record DeleteAccountCommand(Guid AccountId) : ICommand<Guid>;
}
