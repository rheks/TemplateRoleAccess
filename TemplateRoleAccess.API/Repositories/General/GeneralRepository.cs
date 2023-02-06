using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TemplateRoleAccess.API.Models.Context;
using TemplateRoleAccess.API.Repositories.Interfaces;

namespace TemplateRoleAccess.API.Repositories.General
{
    public class GeneralRepository<Entity, Key> : IGeneralRepository<Entity, Key>
        where Entity : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Entity> _entities;
        public GeneralRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<Entity>();
        }

        public async Task<IEnumerable<Entity>> Get()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Entity> Get(Key key)
        {
            return await _entities.FindAsync(key);
        }

        public async Task<int> Post(Entity entity)
        {
            await _entities.AddAsync(entity);
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Entity entity)
        {
            _entities.Update(entity);
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(Key key)
        {
            _entities.Remove(await _entities.FindAsync(key));
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
