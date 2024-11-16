using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Interfaces.Repositories;
using TaskManagement.Data.DbContexts;
using TaskManagement.Data.Repositories;

namespace TaskManagement.Data;

public class UnitOfWork : IUnitOfWork
{
    #region Private Fields

    private ApplicationDbContext dbContext;

    private ITaskRepository? taskRepository;

    private IRepositoryBase<Core.Models.TaskStatus> taskStatusRepository;

    #endregion

    #region Public Constructors

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    #endregion

    #region Task Repositories

    public ITaskRepository TaskRepository
    {
        get
        {
            if (this.taskRepository == null)
            {
                this.taskRepository = new TaskRepository(this.dbContext);
            }

            return this.taskRepository;
        }
    }

    public IRepositoryBase<Core.Models.TaskStatus> TaskStatusRepository
    {
        get
        {
            if (this.taskStatusRepository == null)
            {
                this.taskStatusRepository = new RepositoryBase<Core.Models.TaskStatus>(this.dbContext);
            }

            return this.taskStatusRepository;
        }
    }

    #endregion

    #region Public Methods

    public void Save()
    {
        this.dbContext.SaveChanges();
    }

    #endregion

}