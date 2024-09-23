using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.RejectTransaction
{
    internal sealed class RejectTransactionCommandHandler : ICommandHandler<RejectTransactionCommand, Guid>
    {
        private readonly ITransactionSqlRepository _transactionSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectTransactionCommandHandler(ITransactionSqlRepository transactionSqlRepository, IUnitOfWork unitOfWork)
        {
            _transactionSqlRepository = transactionSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RejectTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(request.TransactionId, cancellationToken);

            if (transaction == null)
            {
                return Result.Failure<Guid>(TransactionErrors.NotFound);
            }

            var result = transaction.Reject();

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(TransactionErrors.NotRejected);
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
