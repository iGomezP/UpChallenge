using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Application.Accounts.Queries.GetAccountsByCustomer
{
    public sealed class GetAccountByCustomerQueryHandler : IQueryHandler<GetAccountByCustomerQuery, AccountDto>
    {
        private readonly IAccountMongoRepository _accountReadRepository;

        public GetAccountByCustomerQueryHandler(IAccountMongoRepository accountReadRepository)
        {
            _accountReadRepository = accountReadRepository;
        }

        public async Task<Result<AccountDto>> Handle(GetAccountByCustomerQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountReadRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (accounts == null)
            {
                return Result.Failure<AccountDto>(AccountErrors.NotFound);
            }

            return Result.Success(accounts);
        }
    }
}
