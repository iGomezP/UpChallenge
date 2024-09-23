using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.FailTransaction
{
    internal sealed class FailTransactionCommandHandler : ICommandHandler<FailTransactionCommand, Guid>
    {
        private readonly ITransactionSqlRepository _transactionSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FailTransactionCommandHandler(ITransactionSqlRepository transactionSqlRepository, IUnitOfWork unitOfWork)
        {
            _transactionSqlRepository = transactionSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(FailTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(request.TransactionId, cancellationToken);

            if (transaction == null)
            {
                return Result.Failure<Guid>(TransactionErrors.NotFound);
            }

            var result = transaction.Fail();

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(TransactionErrors.NotFailed);
            }

            try
            {
                _transactionSqlRepository.Update(transaction);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success(transaction.Id);
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(TransactionErrors.GeneralFailure);
            }
        }
    }
}
