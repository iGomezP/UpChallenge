using UpBack.Application.Abstractions.Clock;
using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.Customers;

namespace UpBack.Application.Accounts.Commands.DeleteAccount
{
    internal sealed class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountSqlRepository _accountRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DeleteAccountCommandHandler(IUnitOfWork unitOfWork, IAccountSqlRepository accountRepository, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);
            if (account == null)
            {
                return Result.Failure<Guid>(CustomerErrors.NotFound);
            }

            var closeResult = account.CloseAccount();
            if (closeResult.IsFailure)
            {
                return Result.Failure<Guid>(AccountErrors.BalanceNotZero);
            }

            try
            {
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
