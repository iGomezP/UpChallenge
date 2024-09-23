using FluentValidation;

namespace UpBack.Application.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty().WithMessage("Account ID is required.");
        }
    }
}
