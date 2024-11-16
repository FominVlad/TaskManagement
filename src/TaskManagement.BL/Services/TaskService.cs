using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.DTO;
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

    public async Task<List<GetTaskFullInfoDto>> GetAllTasksFullInfo()
    {
        return await this.unitOfWork.TaskRepository.GetAllTasksFullInfo();
    }

    #endregion

}