using Microsoft.AspNetCore.Mvc;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly DataContext _dataContext;
    
    public ProjectController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    //View part
    [HttpGet("GetAll")]
    public ActionResult<Project> Get()
    {
        IEnumerable<Project> projects = _dataContext.Projects;

        return Ok(projects);
    }

    [HttpGet("GetById")]
    public ActionResult<Project> GetSingle(int? id)
    {
        if (id == null || id > _dataContext.Projects.Count())
        {
            return NotFound();
        }
        var projectFromDatabase = _dataContext.Projects.Find(id);
        
        return Ok(projectFromDatabase);
    }

    //Creat part
    [HttpPost("Create")]
    public ActionResult<Project> Create(string name, DateTime startDate, DateTime completionDate, string selectedStatus, int priority)
    {
        var newProject = new Project(){Name = name, StartDate = startDate, CompletionDate = completionDate, SelectedProjectStatus = selectedStatus, Priority = priority};
        
        _dataContext.Add(newProject);
        _dataContext.SaveChanges();
            
        return Ok(newProject);
    }
    
    //Edit part
    [HttpPut("Edit")]
    public ActionResult<Project> Edit(int id ,string name, DateTime startDate, DateTime completionDate, string selectedProjectSelectedStatus, int priority)
    {
        var editProject = new Project(){Id = id, Name = name, StartDate = startDate, CompletionDate = completionDate, SelectedProjectStatus = selectedProjectSelectedStatus, Priority = priority};
        
        _dataContext.Update(editProject);
        _dataContext.SaveChanges();

        return Ok(editProject);
    }
    
    //Delete part
    [HttpDelete("DeleteById")]
    public ActionResult<Project> DeleteProject(int? id)
    {
        if (id == null || id > _dataContext.Projects.Count())
        {
            return NotFound();
        }
        var deleteProject = _dataContext.Projects.Find(id);
        
        _dataContext.Remove(deleteProject);
        _dataContext.SaveChanges();

        return Ok(deleteProject);
    }
    
    //Part Create(Add tasks in project)
    [HttpPost("Add/Task")]
    public ActionResult<TaskItem> CreateTask(string name, string description, string selectedStatus, int priority, int projectId)
    {
        var newTask = new TaskItem(){Name = name, Description = description, ProjectId = projectId, SelectedTaskStatus = selectedStatus, Priority = priority};
        
        _dataContext.Add(newTask);
        _dataContext.SaveChanges();

        return Ok(newTask);
    }
    
    //Part : Delete Task from project
    [HttpDelete("Delete/Task")]
    public ActionResult<TaskItem> DeleteTask(int? id)
    {
        if (id == null || id > _dataContext.TaskItems.Count())
        {
            return NotFound();
        }

        var deleteTaskFromProject = _dataContext.TaskItems.Find(id);
        _dataContext.Remove(deleteTaskFromProject);
        _dataContext.SaveChanges();

        return Ok(deleteTaskFromProject);
    }
    
    //Part : View all tasks in the Project
    [HttpGet("GetAllTasksFromProject")]
    public ActionResult<TaskItem> GetAllTasks(int projectId)
    {
        var taskItems = _dataContext.TaskItems;
        var filterByProjectId = from t in taskItems where t.ProjectId == projectId select t;

        return Ok(filterByProjectId);
    }

    [HttpGet("FilterByProjectPriority")]
    public IActionResult FilterByProjectPriority(int priority)
    {
        var listOfProjects = _dataContext.Projects;
        var sortedProjectsList = from p in listOfProjects where p.Priority == priority select p;

        return Ok(sortedProjectsList);
    }
    
    
    [HttpGet("FilterByStartDate")]
    public IActionResult FilterByStartDate(DateTime dateTime)
    {
        var listOfProjects = _dataContext.Projects;
        var sortedProjectsList = from p in listOfProjects where p.StartDate == dateTime select p;

        return Ok(sortedProjectsList);
    }
    
    [HttpGet("SortByName")]
    public IActionResult SortByName()
    {
        var listOfProjects = _dataContext.Projects;
        var sortedProjectsList = from p in listOfProjects orderby p.Name select p;

        return Ok(sortedProjectsList);
    }

    [HttpPost("FilteringByName")]
    public IActionResult FilteringByName(string filterParam)
    {
        var listOfProject = _dataContext.Projects;
        var filteredList = from p in listOfProject where p.Name == filterParam select p;
        
        return Ok(filteredList);
    }
}