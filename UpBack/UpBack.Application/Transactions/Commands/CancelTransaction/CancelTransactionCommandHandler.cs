using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.CancelTransaction
{
    internal sealed class CancelTransactionCommandHandler : ICommandHandler<CancelTransactionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionSqlRepository _transactionSqlRepository;

        public CancelTransactionCommandHandler(IUnitOfWork unitOfWork, ITransactionSqlRepository transactionSqlRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionSqlRepository = transactionSqlRepository;
        }

        public async Task<Result<Guid>> Handle(CancelTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(request.TransactionId, cancellationToken);

            if (transaction is null)
            {
                return Result.Failure<Guid>(TransactionErrors.NotFound);
            }

            var result = transaction.Cancel();
            if (result.IsFailure)
            {
                return Result.Failure<Guid>(TransactionErrors.NotCancelled);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(transaction.Id);
        }
    }
}
