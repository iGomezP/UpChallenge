using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Application.Accounts.Queries.GetAllAccounts
{
    public sealed class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IAccountMongoRepository _accountReadRepository;

        public GetAllAccountsQueryHandler(IAccountMongoRepository accountReadRepository)
        {
            _accountReadRepository = accountReadRepository;
        }

        public async Task<Result<IEnumerable<AccountDto>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountReadRepository.GetAllAsync(cancellationToken);

            if (accounts == null || !accounts.Any())
            {
                return Result.Failure<IEnumerable<AccountDto>>(AccountErrors.NotFound);
            }

            return Result.Success(accounts);
        }
    }
}
