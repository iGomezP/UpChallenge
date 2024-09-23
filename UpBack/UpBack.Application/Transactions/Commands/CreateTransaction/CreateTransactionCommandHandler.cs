using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.ObjectValues;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Application.Transactions.Commands.CreateTransaction
{
    internal sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionSqlRepository _transactionSqlRepository;
        private readonly IAccountSqlRepository _accountRepository;

        public CreateTransactionCommandHandler(
            IUnitOfWork unitOfWork,
            ITransactionSqlRepository transactionSqlRepository,
            IAccountSqlRepository accountRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionSqlRepository = transactionSqlRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

            if (account is null)
            {
                return Result.Failure<Guid>(TransactionErrors.AccountNotFound);
            }

            var transaction = Transaction.Create(
                TransactionType.Create(request.Type.Value),
                request.AccountId,
                TransactionQuantity.Create(request.Quantity),
                request.TransactionDate,
                account,
                request.Reference,
                request.Status
            );

            try
            {
                _transactionSqlRepository.Add(transaction);
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
