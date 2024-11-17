namespace TaskManagement.Core.DTO;

public class CreateTaskDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string? AssignedTo { get; set; }
}