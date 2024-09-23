using MongoDB.Driver;
using UpBack.Domain.Abstractions.Dtos;
using UpBack.Domain.Roles.Repositories;

namespace UpBack.Infrastructure.Repositories
{
    public class RoleMongoRepository : IRoleMongoRepository
    {
        private readonly IMongoCollection<RoleDto> _rolesCollection;

        public RoleMongoRepository(IMongoDatabase database)
        {
            _rolesCollection = database.GetCollection<RoleDto>("Roles");
        }

        public async Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _rolesCollection.Find(role => role.Id == id).FirstOrDefaultAsync();
        }

        public async Task<RoleDto> GetByTitleAsync(string title)
        {
            return await _rolesCollection.Find(role => role.Title == title).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            return await _rolesCollection.Find(role => true).ToListAsync();
        }

        public async Task AddAsync(RoleDto role, CancellationToken cancellationToken)
        {
            await _rolesCollection.InsertOneAsync(role, null, cancellationToken);
        }

        public async Task UpdateAsync(RoleDto role)
        {
            await _rolesCollection.ReplaceOneAsync(r => r.Id == role.Id, role);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _rolesCollection.DeleteOneAsync(role => role.Id == id);
        }
    }
}
