namespace TaskTracker.Models;

/// <summary>
/// Properties of Task Entity
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? SelectedTaskStatus { get; set; }
    
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}