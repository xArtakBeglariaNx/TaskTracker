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

    [HttpGet("id")]
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
    [HttpPost]
    public ActionResult<Project> Create(Project newProject)
    {
        _dataContext.Add(newProject);
        _dataContext.SaveChanges();
            
        return Ok(newProject);
    }
    
    //Edit part
    [HttpPut]
    public ActionResult<Project> Edit(Project editProject)
    {
        _dataContext.Update(editProject);
        _dataContext.SaveChanges();

        return Ok(editProject);
    }
    
    //Delete part
    [HttpDelete("id")]
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
    [HttpPost("add/Task")]
    public ActionResult<TaskItem> CreateTask(TaskItem newTask)
    {
        _dataContext.Add(newTask);
        _dataContext.SaveChanges();

        return Ok(newTask);
    }
    
    //Part : Delete Task from project
    [HttpDelete("delete/Task")]
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
    [HttpGet("GetAllTasks")]
    public ActionResult<TaskItem> GetAllTasks()
    {
        IEnumerable<TaskItem> taskItems = _dataContext.TaskItems;

        return Ok(taskItems);
    }
}