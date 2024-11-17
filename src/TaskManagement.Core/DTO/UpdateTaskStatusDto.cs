namespace TaskManagement.Core.DTO;

public class UpdateTaskStatusDto
{
    public int TaskId { get; set; }

    public Enums.TaskStatus Status { get; set; }
}