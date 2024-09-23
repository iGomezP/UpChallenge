using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.Customers;
using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Accounts.Commands.TransferToAccount
{
    internal sealed class TransferToAccountCommandHandler : ICommandHandler<TransferToAccountCommand, Guid>
    {
        private readonly IAccountSqlRepository _accountWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransferToAccountCommandHandler(IAccountSqlRepository accountWriteRepository, IUnitOfWork unitOfWork)
        {
            _accountWriteRepository = accountWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(TransferToAccountCommand request, CancellationToken cancellationToken)
        {
            var sourceAccount = await _accountWriteRepository.GetAccountByIdAsync(request.SourceAccountId, cancellationToken);
            var targetAccount = await _accountWriteRepository.GetAccountByIdAsync(request.TargetAccountId, cancellationToken);

            if (sourceAccount == null || targetAccount == null)
            {
                return Result.Failure<Guid>(AccountErrors.NotFound);
            }

            try
            {
                var transferResult = sourceAccount.TransferTo(
                    targetAccount,
                    AccountBalance.Create(request.Amount).Value,
                    request.Reference
                    );

                if (transferResult.IsFailure)
                {
                    return Result.Failure<Guid>(transferResult.Error);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success(sourceAccount.Id);
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(CustomerErrors.GeneralFailure);
            }
        }
    }
}
