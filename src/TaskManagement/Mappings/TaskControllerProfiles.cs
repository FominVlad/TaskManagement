using AutoMapper;
using TaskManagement.Core.DTO;
using TaskManagement.Models.TaskController.RequestModels;
using TaskManagement.Models.TaskController.ResponseModels;

namespace TaskManagement.Mappings;

public class TaskControllerProfiles : Profile
{
    public TaskControllerProfiles()
    {
        this.CreateMap<UpdateTaskStatusRequest, UpdateTaskStatusDto>();

        this.CreateMap<GetTaskFullInfoDto, GetListOfAllTasksResponse>();

        this.CreateMap<Core.Models.Task, CreateTaskResponse>();

        this.CreateMap<CreateTaskRequest, CreateTaskDto>();
    }
}