using FluentValidation;

namespace UpBack.Application.Transactions.Commands.RejectTransaction
{
    public class RejectTransactionCommandValidator : AbstractValidator<RejectTransactionCommand>
    {
        public RejectTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId cannot be empty.");
        }
    }
}
