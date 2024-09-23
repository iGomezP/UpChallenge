using FluentValidation;

namespace UpBack.Application.Accounts.Commands.DepositAccount
{
    public sealed class DepositAccountCommandValidator : AbstractValidator<DepositAccountCommand>
    {
        public DepositAccountCommandValidator()
        {
            RuleFor(c => c.AccountId).NotEmpty().WithMessage("AccountId is required.");
            RuleFor(c => c.NewBalance).GreaterThanOrEqualTo(0).WithMessage("New balance cannot be negative.");
        }
    }
}
