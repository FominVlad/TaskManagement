using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Core.Models;

public class TaskStatus
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}