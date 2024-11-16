using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Interfaces;
using TaskManagement.Data.DbContexts;

namespace TaskManagement.Data.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class
{
    public RepositoryBase(ApplicationDbContext dbContext)
    {
        this.DbContext = dbContext;
    }

    protected ApplicationDbContext DbContext { get; set; }

    protected DbSet<TEntity> Set => this.DbContext.Set<TEntity>();

    public void Create(TEntity entity)
    {
        this.Set.Add(entity);
    }

    public void CreateMany(IEnumerable<TEntity> entities)
    {
        this.Set.AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        this.Set.Update(entity);
    }

    public void UpdateMany(IEnumerable<TEntity> entities)
    {
        this.Set.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        this.Set.Remove(entity);
    }

    public void DeleteMany(IEnumerable<TEntity> entities)
    {
        this.Set.RemoveRange(entities);
    }

    public async Task<TEntity> GetAsync(int id)
    {
        return await this.Set.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await this.Set.ToListAsync();
    }
}