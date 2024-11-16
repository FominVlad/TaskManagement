using TaskManagement.Core.DTO;

namespace TaskManagement.Core.Interfaces.Services;

public interface ITaskService
{
    Task<List<GetTaskFullInfoDto>> GetAllTasksFullInfo();
}