using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.TaskController.RequestModels;

public class CreateTaskRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string Description { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public string? AssignedTo { get; set; }
}