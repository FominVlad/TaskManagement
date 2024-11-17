namespace TaskManagement.Core.DTO;

public class TaskProcessingDto
{
    public int Id { get; set; }

    public Enums.TaskStatus Status { get; set; }
}