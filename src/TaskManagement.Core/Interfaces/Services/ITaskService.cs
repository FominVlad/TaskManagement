using TaskManagement.Core.DTO;

namespace TaskManagement.Core.Interfaces.Services;

public interface ITaskService
{
    Task<Models.Task> CreateTask(CreateTaskDto createTaskDto);

    Task UpdateTaskStatus(UpdateTaskStatusDto updateTaskStatusDto);

    Task<List<GetTaskFullInfoDto>> GetAllTasksFullInfo();

    Task ProcessTask(TaskProcessingDto taskProcessingDto);
}