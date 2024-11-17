using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace TaskManagement.Models.TaskController.RequestModels;

public class UpdateTaskStatusRequest
{
    [Required]
    [Min(1)]
    public int TaskId { get; set; }

    [Required]
    public Core.Enums.TaskStatus Status { get; set; }
}