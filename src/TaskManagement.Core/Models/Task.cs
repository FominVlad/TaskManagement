using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Core.Models;

public class Task
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Enums.TaskStatus Status { get; set; }

    public string? AssignedTo { get; set; }

    [ForeignKey(nameof(Status))]
    public virtual TaskStatus? TaskStatus { get; set; }
}