using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models;

/// <summary>
/// Properties of Task Entity
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    
    //This Id need for linked Project entity and Tasks Entity
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    //This property need for get selected status from TaskStatus
    [Required]
    public string? SelectedTaskStatus { get; set; }
    [Required]
    public int Priority { get; set; }
    
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}