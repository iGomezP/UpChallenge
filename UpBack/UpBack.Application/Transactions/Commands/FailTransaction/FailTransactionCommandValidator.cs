using FluentValidation;

namespace UpBack.Application.Transactions.Commands.FailTransaction
{
    public class FailTransactionCommandValidator : AbstractValidator<FailTransactionCommand>
    {
        public FailTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId cannot be empty.");
        }
    }
}
