using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.CompleteTransaction
{
    internal sealed class CompleteTransactionCommandHandler : ICommandHandler<CompleteTransactionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionSqlRepository _transactionSqlRepository;

        public CompleteTransactionCommandHandler(IUnitOfWork unitOfWork, ITransactionSqlRepository transactionSqlRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionSqlRepository = transactionSqlRepository;
        }

        public async Task<Result<Guid>> Handle(CompleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(request.TransactionId, cancellationToken);

            if (transaction is null)
            {
                return Result.Failure<Guid>(TransactionErrors.NotFound);
            }

            var result = transaction.Complete();
            if (result.IsFailure)
            {
                return Result.Failure<Guid>(TransactionErrors.NotCompleted);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(transaction.Id);
        }
    }
}
