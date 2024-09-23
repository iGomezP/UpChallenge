using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.UpdateTransaction
{
    internal sealed class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionSqlRepository _transactionSqlRepository;

        public UpdateTransactionCommandHandler(
            IUnitOfWork unitOfWork,
            ITransactionSqlRepository transactionSqlRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionSqlRepository = transactionSqlRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionSqlRepository.GetTransactionByIdAsync(request.TransactionId, cancellationToken);

            if (transaction is null)
            {
                return Result.Failure<Guid>(TransactionErrors.NotFound);
            }

            transaction.UpdateDetails(
                TransactionQuantity.Create(request.NewQuantity),
                request.NewReference,
                request.NewStatus
            );

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
