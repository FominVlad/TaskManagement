namespace TaskManagement.Models.TaskController.ResponseModels;

public class CreateTaskResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Status { get; set; }

    public string? AssignedTo { get; set; }
}