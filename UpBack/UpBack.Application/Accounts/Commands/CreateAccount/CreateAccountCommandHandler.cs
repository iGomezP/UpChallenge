using UpBack.Application.Abstractions.Clock;
using UpBack.Application.Abstractions.Messaging;
using UpBack.Application.Exceptions;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;
using UpBack.Domain.ObjectValues;

namespace UpBack.Application.Accounts.Commands.CreateAccount
{
    public sealed class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountSqlRepository _accountRepository;
        private readonly ICustomerSqlRepository _customerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateAccountCommandHandler(
            IUnitOfWork unitOfWork,
            IAccountSqlRepository accountRepository,
            IDateTimeProvider dateTimeProvider,
            ICustomerSqlRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _dateTimeProvider = dateTimeProvider;
            _customerRepository = customerRepository;
        }

        public async Task<Result<Guid>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
            {
                return Result.Failure<Guid>(CustomerErrors.NotFound);
            }

            try
            {
                var account = Account.Create(
                    request.CustomerId,
                    AccountBalance.Create(request.InitialBalance).Value,
                    "System",
                    customer,
                    _dateTimeProvider.CurrentTime,
                    "active"
                    );

                _accountRepository.Add(account);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return account.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(AccountErrors.GeneralFailure);
            }
        }
    }
}
