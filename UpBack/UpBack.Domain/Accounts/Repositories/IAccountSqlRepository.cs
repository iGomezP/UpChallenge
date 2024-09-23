namespace UpBack.Domain.Accounts.Repositories
{
    public interface IAccountSqlRepository
    {
        Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Account?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        Task<Account?> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
