namespace MultiContactManagement.Domain.Interfaces.InterfacesExternal
{
    public interface IRepositoryBaseExternal<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
    }
}
