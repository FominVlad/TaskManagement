﻿namespace TaskManagement.Core.DTO;

public class GetTaskFullInfoDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Enums.TaskStatus Status { get; set; }

    public string StatusName { get; set; }

    public string? AssignedTo { get; set; }
}