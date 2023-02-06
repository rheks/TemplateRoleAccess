namespace TemplateRoleAccess.API.Repositories.Interfaces
{
    public interface IGeneralRepository<Entity, Key> where Entity : class
    {
        Task<int> Delete(Key key);
        Task<IEnumerable<Entity>> Get();
        Task<Entity> Get(Key key);
        Task<int> Post(Entity entity);
        Task<int> Update(Entity entity);
    }
}