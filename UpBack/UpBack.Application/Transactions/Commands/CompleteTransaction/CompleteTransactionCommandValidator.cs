using FluentValidation;

namespace UpBack.Application.Transactions.Commands.CompleteTransaction
{
    public sealed class CompleteTransactionCommandValidator : AbstractValidator<CompleteTransactionCommand>
    {
        public CompleteTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty();
        }
    }
}
