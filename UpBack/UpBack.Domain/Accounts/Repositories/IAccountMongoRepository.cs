using UpBack.Domain.Abstractions.Dtos;

namespace UpBack.Domain.Accounts.Repositories
{
    public interface IAccountMongoRepository
    {
        Task<AccountDto?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task<AccountDto> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(AccountDto account, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid accountId, CancellationToken cancellationToken = default);
        Task UpdateAsync(AccountDto account, CancellationToken cancellationToken = default);
        Task<AccountDto?> GetByNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    }
}
