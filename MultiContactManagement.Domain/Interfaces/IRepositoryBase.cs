namespace MultiContactManagement.Domain.Interfaces
{
    public interface IRepositoryBaseExternal<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
    }
}
