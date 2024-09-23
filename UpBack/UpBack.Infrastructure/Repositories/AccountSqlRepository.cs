using Microsoft.EntityFrameworkCore;
using UpBack.Domain.Accounts;
using UpBack.Domain.Accounts.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    internal sealed class AccountSqlRepository(ApplicationDBContext _dbContext) : SqlRepository<Account>(_dbContext), IAccountSqlRepository
    {
        public async Task<Account?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Account>()
            .FirstOrDefaultAsync(account => account.CustomerId == customerId, cancellationToken);
        }

        public async Task<Account?> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Account>()
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        //public new void Update(Account account)
        //{
        //    if (account.Reference == null || string.IsNullOrWhiteSpace(account.Reference.Value))
        //    {
        //        throw new Exception("El campo Reference en la cuenta origen es nulo o está vacío.");
        //    }
        //    _dbContext.Set<Account>().Update(account);
        //    _dbContext.SaveChanges();
        //}
    }
}
