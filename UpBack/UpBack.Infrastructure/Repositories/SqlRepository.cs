using Microsoft.EntityFrameworkCore;
using UpBack.Domain.Abstractions;

namespace UpBack.Infrastructure.Repositories
{
    internal abstract class SqlRepository<T>(ApplicationDBContext _dbContext)
        where T : Entity
    {
        protected readonly ApplicationDBContext _dbContext = _dbContext;


        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
