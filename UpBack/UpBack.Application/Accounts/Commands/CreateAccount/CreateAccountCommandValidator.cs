using FluentValidation;

namespace UpBack.Application.Accounts.Commands.CreateAccount
{
    public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(c => c.InitialBalance).GreaterThanOrEqualTo(0).WithMessage("Initial balance cannot be negative.");
            RuleFor(c => c.ObjectStatus).Must(s => s == "active" || s == "inactive")
                .WithMessage("ObjectStatus must be either 'active' or 'inactive'.");
        }
    }

}
