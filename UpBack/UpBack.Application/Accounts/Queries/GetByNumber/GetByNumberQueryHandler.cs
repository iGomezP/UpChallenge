using UpBack.Application.Abstractions.Messaging;
using UpBack.Domain.Abstractions;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Application.Accounts.Queries.GetByNumber
{
    internal sealed class GetByNumberQueryHandler : IQueryHandler<GetByNumberQuery, AccountDto>
    {
        private readonly IAccountMongoRepository _accountReadRepository;

        public GetByNumberQueryHandler(IAccountMongoRepository accountReadRepository)
        {
            _accountReadRepository = accountReadRepository;
        }

        public async Task<Result<AccountDto>> Handle(GetByNumberQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountReadRepository.GetByNumberAsync(request.accountNumber, cancellationToken);

            if (account == null)
            {
                return Result.Failure<AccountDto>(AccountErrors.NotFound);
            }

            return Result.Success(account);
        }
    }
}
