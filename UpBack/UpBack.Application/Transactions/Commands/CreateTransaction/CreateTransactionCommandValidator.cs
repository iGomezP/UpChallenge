
using FluentValidation;

namespace UpBack.Application.Transactions.Commands.CreateTransaction
{
    public sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Reference).NotEmpty().MaximumLength(30);
        }
    }
}
