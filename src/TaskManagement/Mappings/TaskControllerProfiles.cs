using AutoMapper;
using TaskManagement.Core.DTO;
using TaskManagement.Models.TaskController.ResponseModels;

namespace TaskManagement.Mappings;

public class TaskControllerProfiles : Profile
{
    public TaskControllerProfiles()
    {
        this.CreateMap<GetTaskFullInfoDto, GetListOfAllTasksResponse>();
    }
}