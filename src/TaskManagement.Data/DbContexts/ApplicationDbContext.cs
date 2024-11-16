using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Data.DbContexts;

public class ApplicationDbContext : DbContext
{
    #region Public Constructors

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    #endregion

    #region DbSets

    public DbSet<Core.Models.Task> Tasks { get; set; }

    public DbSet<Core.Models.TaskStatus> TaskStatuses { get; set; }

    #endregion

    #region Db Configuration

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Models.TaskStatus>().HasData(
            new Core.Models.TaskStatus { Id = 1, Name = "Not Started", Description = "The task has not yet been started." },
            new Core.Models.TaskStatus { Id = 2, Name = "In Progress", Description = "The task is currently in progress." },
            new Core.Models.TaskStatus { Id = 3, Name = "Completed", Description = "The task has been completed." });
    }

    #endregion

}