using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.DTO;
using TaskManagement.Core.Interfaces.Services;
using TaskManagement.Models;
using TaskManagement.Models.TaskController.RequestModels;
using TaskManagement.Models.TaskController.ResponseModels;

namespace TaskManagement.Controllers;

[Route("/api/v1/task")]
public class TaskController : ApiControllerBase
{
    #region Private Fields

    private readonly ITaskService taskService;
    private readonly IMapper mapper;

    #endregion

    #region Public Constructors

    public TaskController(
        ITaskService taskService,
        IMapper mapper)
    {
        this.taskService = taskService;
        this.mapper = mapper;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// To create a new task.
    /// </summary>
    /// <param name="request">Task parameters.</param>
    /// <returns>Created task parameters.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        CreateTaskDto taskDto = this.mapper.Map<CreateTaskDto>(request);

        Core.Models.Task task = await this.taskService.CreateTask(taskDto);

        return new JsonResult(this.mapper.Map<CreateTaskResponse>(task));
    }

    /// <summary>
    /// To update task status.
    /// </summary>
    /// <param name="request">Task parameters.</param>
    /// <returns>NoContent.</returns>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusRequest request)
    {
        UpdateTaskStatusDto updateTaskStatusDto = this.mapper.Map<UpdateTaskStatusDto>(request);

        await this.taskService.UpdateTaskStatus(updateTaskStatusDto);

        return this.NoContent();
    }

    /// <summary>
    /// To get all tasks in system.
    /// </summary>
    /// <returns>Return list of objects with existing tasks.</returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(GetListOfAllTasksResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> GetListOfAllTasks()
    {
        List<GetTaskFullInfoDto> taskDtos = await this.taskService.GetAllTasksFullInfo();

        return new JsonResult(this.mapper.Map<List<GetListOfAllTasksResponse>>(taskDtos));
    }

    #endregion

}