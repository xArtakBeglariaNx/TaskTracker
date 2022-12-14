namespace TaskTracker.Models;

/// <summary>
/// Properties of Task Entity
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    
    //This Id need for linked Project entity and Tasks Entity
    public int ProjectId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    //This property need for get selected status from TaskStatus
    public string? SelectedTaskStatus { get; set; }
    public int Priority { get; set; }
    
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}