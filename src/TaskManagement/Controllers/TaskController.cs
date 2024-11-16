using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.DTO;
using TaskManagement.Core.Interfaces.Services;
using TaskManagement.Models;
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

        List<GetListOfAllTasksResponse> response = this.mapper.Map<List<GetListOfAllTasksResponse>>(taskDtos);

        return new JsonResult(response);
    }

    #endregion

}