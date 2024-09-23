using FluentValidation;

namespace UpBack.Application.Accounts.Commands.WithdrawAccount
{
    public sealed class WithdrawAccountCommandValidator : AbstractValidator<WithdrawAccountCommand>
    {
        public WithdrawAccountCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
