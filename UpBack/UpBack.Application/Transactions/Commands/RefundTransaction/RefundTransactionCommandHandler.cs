using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.RefundTransaction
{
    internal sealed class RefundTransactionCommandHandler : ICommandHandler<RefundTransactionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionSqlRepository _transactionSqlRepository;

        public RefundTransactionCommandHandler(IUnitOfWork unitOfWork, ITransactionSqlRepository transactionSqlRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionSqlRepository = transactionSqlRepository;
        }

        public async Task<Result<Guid>> Handle(RefundTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(request.TransactionId, cancellationToken);

            if (transaction is null)
            {
                return Result.Failure<Guid>(TransactionErrors.NotFound);
            }

            var result = transaction.Refund();
            if (result.IsFailure)
            {
                return Result.Failure<Guid>(TransactionErrors.NotRefunded);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(transaction.Id);
        }
    }
}
