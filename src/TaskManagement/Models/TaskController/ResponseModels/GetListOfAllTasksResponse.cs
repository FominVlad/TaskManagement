﻿namespace TaskManagement.Models.TaskController.ResponseModels;

public class GetListOfAllTasksResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Core.Enums.TaskStatus Status { get; set; }

    public string StatusName { get; set; }

    public string? AssignedTo { get; set; }
}