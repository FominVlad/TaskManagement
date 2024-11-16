using TaskManagement.Core.DTO;

namespace TaskManagement.Core.Interfaces.Repositories;

public interface ITaskRepository : IRepositoryBase<Models.Task>
{
    Task<List<GetTaskFullInfoDto>> GetAllTasksFullInfo();
}