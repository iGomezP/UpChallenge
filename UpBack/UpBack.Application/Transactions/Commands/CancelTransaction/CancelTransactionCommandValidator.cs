using FluentValidation;

namespace UpBack.Application.Transactions.Commands.CancelTransaction
{
    public sealed class CancelTransactionCommandValidator : AbstractValidator<CancelTransactionCommand>
    {
        public CancelTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty();
        }
    }
}
