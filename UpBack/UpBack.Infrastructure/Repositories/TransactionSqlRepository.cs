using Microsoft.EntityFrameworkCore;
using UpBack.Domain.Transactions;
using UpBack.Domain.Transactions.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    internal sealed class TransactionSqlRepository(ApplicationDBContext _dbContext) : SqlRepository<Transaction>(_dbContext), ITransactionSqlRepository
    {
        public async Task<Transaction?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Transaction>()
                .Include(a => a.Account)
                .ThenInclude(t => t.Customer)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}
