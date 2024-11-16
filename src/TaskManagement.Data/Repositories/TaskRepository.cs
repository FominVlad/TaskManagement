using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.DTO;
using TaskManagement.Core.Interfaces.Repositories;
using TaskManagement.Data.DbContexts;

namespace TaskManagement.Data.Repositories;

public class TaskRepository : RepositoryBase<Core.Models.Task>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext dbContext)
    : base(dbContext)
    {
    }

    public async Task<List<GetTaskFullInfoDto>> GetAllTasksFullInfo()
    {
        return await this.Set
            .Select(t => new GetTaskFullInfoDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Status = t.Status,
                StatusName = t.TaskStatus.Name,
                AssignedTo = t.AssignedTo,
            })
            .ToListAsync();
    }
}