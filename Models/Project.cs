namespace TaskTracker.Models;

/// <summary>
/// Properties of Project Entity
/// </summary>
public class Project
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now.Date;
    public DateTime CompletionDate { get; set; } = DateTime.Today.Date;
    
    //This property need for get selected status from ProjectStatus
    public string? SelectedProjectStatus { get; set; }
    public int Priority { get; set; }
    
    public enum ProjectStatus
    {
        NotStarted,
        Active,
        Completed
    }
}