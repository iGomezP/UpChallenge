using FluentValidation;

namespace UpBack.Application.Accounts.Commands.TransferToAccount
{
    public sealed class TransferToAccountCommandValidator : AbstractValidator<TransferToAccountCommand>
    {
        public TransferToAccountCommandValidator()
        {
            RuleFor(x => x.SourceAccountId).NotEmpty();
            RuleFor(x => x.TargetAccountId).NotEmpty();
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
