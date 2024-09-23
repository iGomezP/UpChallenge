using UpBack.Domain.Roles;
using UpBack.Domain.Roles.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    internal sealed class RoleSqlRepository(ApplicationDBContext _dbContext) : SqlRepository<Role>(_dbContext), IRoleSqlRepository
    {
    }
}
