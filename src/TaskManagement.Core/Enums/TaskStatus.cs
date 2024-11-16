namespace TaskManagement.Core.Enums;

public enum TaskStatus
{
    /// <summary>
    /// The task has not yet been started.
    /// </summary>
    NotStarted = 1,

    /// <summary>
    /// The task is currently in progress.
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// The task has been completed.
    /// </summary>
    Completed = 3,
}