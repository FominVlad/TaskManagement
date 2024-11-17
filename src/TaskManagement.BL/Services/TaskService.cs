using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.DTO;
using TaskManagement.Core.Exceptions;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Interfaces.Services;

namespace TaskManagement.BL.Services;

public class TaskService : ITaskService
{
    #region Private Fields

    private readonly IValidator validator;
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;
    private readonly ILogger<TaskService> logger;

    #endregion

    #region Public Constructors

    public TaskService(
        IValidator validator,
        IUnitOfWork unitOfWork,
        IConfiguration configuration,
        ILogger<TaskService> logger)
    {
        this.validator = validator;
        this.unitOfWork = unitOfWork;
        this.configuration = configuration;
        this.logger = logger;
    }

    #endregion

    #region Public Methods

    public async Task<Core.Models.Task> CreateTask(CreateTaskDto createTaskDto)
    {
        this.validator
            .CheckObjectIsNotNull(createTaskDto, nameof(createTaskDto))
            .CheckStringIsNotNullOrWhiteSpace(createTaskDto.Description, nameof(createTaskDto.Description))
            .CheckStringIsNotNullOrWhiteSpace(createTaskDto.Name, nameof(createTaskDto.Name));

        Core.Models.Task task = new Core.Models.Task
        {
            Name = createTaskDto.Name,
            Description = createTaskDto.Description,
            Status = Core.Enums.TaskStatus.NotStarted,
            AssignedTo = createTaskDto.AssignedTo,
        };

        this.unitOfWork.TaskRepository.Create(task);
        this.unitOfWork.Save();

        this.logger.LogInformation($"New Task \"{task.Name}\" was created (TaskID: {task.Id}).");

        return task;
    }

    public async Task UpdateTaskStatus(UpdateTaskStatusDto updateTaskStatusDto)
    {
        this.validator
            .CheckObjectIsNotNull(updateTaskStatusDto, nameof(updateTaskStatusDto))
            .CheckNumberIsNotZeroOrNegative(updateTaskStatusDto.TaskId, nameof(updateTaskStatusDto.TaskId))
            .CheckValueExistsInEnum(updateTaskStatusDto.Status, typeof(Core.Enums.TaskStatus), nameof(updateTaskStatusDto.Status));

        Core.Models.Task task = await this.unitOfWork.TaskRepository.GetAsync(updateTaskStatusDto.TaskId);

        this.validator
            .CheckObjectIsNotNull(task, nameof(task), $"Task with ID = {updateTaskStatusDto.TaskId} was not found.");

        this.ValidateTaskStatusUpdate(task.Status, updateTaskStatusDto.Status);

        task.Status = updateTaskStatusDto.Status;
        this.unitOfWork.Save();

        this.logger.LogInformation($"Task \"{task.Name}\" was created (TaskID: {task.Id}).");
    }

    public async Task<List<GetTaskFullInfoDto>> GetAllTasksFullInfo()
    {
        return await this.unitOfWork.TaskRepository.GetAllTasksFullInfo();
    }

    #endregion

    #region Private Methods

    private void ValidateTaskStatusUpdate(Core.Enums.TaskStatus currentStatus, Core.Enums.TaskStatus newStatus)
    {
        Dictionary<Core.Enums.TaskStatus, List<Core.Enums.TaskStatus>> allowedTransitions = new()
        {
            {
                Core.Enums.TaskStatus.NotStarted, new List<Core.Enums.TaskStatus>
                {
                    Core.Enums.TaskStatus.InProgress,
                    Core.Enums.TaskStatus.Completed,
                }
            },
            {
                Core.Enums.TaskStatus.InProgress, new List<Core.Enums.TaskStatus>
                {
                    Core.Enums.TaskStatus.Completed,
                }
            },
            {
                Core.Enums.TaskStatus.Completed, new List<Core.Enums.TaskStatus>()
            },
        };

        if (!allowedTransitions[currentStatus].Contains(newStatus))
        {
            throw new InvalidArgumentException($"Invalid status transition from {currentStatus} to {newStatus}.");
        }
    }

    #endregion

}