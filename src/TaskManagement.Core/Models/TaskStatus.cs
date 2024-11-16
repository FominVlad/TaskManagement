using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Core.Models;

public class TaskStatus
{
    [Key]
    [Column(TypeName = "int")]
    public Enums.TaskStatus Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}