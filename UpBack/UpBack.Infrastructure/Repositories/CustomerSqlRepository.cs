using Microsoft.EntityFrameworkCore;
using UpBack.Domain.Customers;
using UpBack.Domain.Customers.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    internal sealed class CustomerSqlRepository(ApplicationDBContext _dbContext) : SqlRepository<Customer>(_dbContext), ICustomerSqlRepository
    {
        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Customer>()
                .AnyAsync(c => c.Email.Value == email, cancellationToken);
        }
    }
}
