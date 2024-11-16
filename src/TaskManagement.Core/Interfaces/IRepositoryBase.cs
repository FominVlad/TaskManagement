namespace TaskManagement.Core.Interfaces;

public interface IRepositoryBase<TEntity>
    where TEntity : class
{
    void Create(TEntity entity);

    void CreateMany(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void UpdateMany(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void DeleteMany(IEnumerable<TEntity> entities);

    Task<TEntity> GetAsync(int id);

    Task<IEnumerable<TEntity>> GetAllAsync();
}