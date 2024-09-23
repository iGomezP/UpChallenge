using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Application.Accounts.Queries.GetAccountById
{
    public sealed class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountDto>
    {
        private readonly IAccountMongoRepository _accountReadRepository;

        public GetAccountByIdQueryHandler(IAccountMongoRepository accountReadRepository)
        {
            _accountReadRepository = accountReadRepository;
        }

        public async Task<Result<AccountDto>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountReadRepository.GetByIdAsync(request.AccountId, cancellationToken);

            if (account == null)
            {
                return Result.Failure<AccountDto>(AccountErrors.NotFound);
            }

            return Result.Success(account);
        }
    }
}
