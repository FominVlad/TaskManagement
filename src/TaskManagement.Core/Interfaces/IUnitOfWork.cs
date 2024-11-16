using TaskManagement.Core.Interfaces.Repositories;

namespace TaskManagement.Core.Interfaces;

public interface IUnitOfWork
{
    #region Task Repositories

    ITaskRepository TaskRepository { get; }

    IRepositoryBase<Models.TaskStatus> TaskStatusRepository { get; }

    #endregion

    void Save();
}