using UpBack.Application.Abstractions.Clock;
using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.Customers;
using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Accounts.Commands.DepositAccount
{
    internal sealed class DepositAccountCommandHandler : ICommandHandler<DepositAccountCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountSqlRepository _accountRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DepositAccountCommandHandler(IUnitOfWork unitOfWork, IAccountSqlRepository accountRepository, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId, cancellationToken);
            if (account == null)
            {
                return Result.Failure<Guid>(CustomerErrors.NotFound);
            }

            var depositAmountResult = AccountBalance.Create(request.NewBalance);
            if (depositAmountResult.IsFailure)
            {
                return Result.Failure<Guid>(depositAmountResult.Error);
            }

            var newReference = request.Reference;

            try
            {
                var depositResult = account.Deposit(depositAmountResult.Value, newReference);
                if (depositResult.IsFailure)
                {
                    return Result.Failure<Guid>(depositResult.Error);
                }

                _accountRepository.Update(account);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success(account.Id);
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(AccountErrors.GeneralFailure);
            }
        }
    }
}
