using FluentValidation;

namespace UpBack.Application.Transactions.Commands.RefundTransaction
{
    public sealed class RefundTransactionCommandValidator : AbstractValidator<RefundTransactionCommand>
    {
        public RefundTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty();
        }
    }
}
