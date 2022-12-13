namespace TaskTracker.Models;

/// <summary>
/// Properties of Project Entity
/// </summary>
public class Project
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    
    public enum ProjectStatus
    {
        NotStarted,
        Active,
        Completed
    }
}