using Microsoft.AspNetCore.Mvc;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly DataContext _dataContext;

    public TaskItemController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    //View part
    [HttpGet("GetAll")]
    public ActionResult<TaskItem> Get()
    {
        IEnumerable<TaskItem> taskItems = _dataContext.TaskItems;

        return Ok(taskItems);
    }

    [HttpGet("GetById")]
    public ActionResult<TaskItem> GetSingle(int? id)
    {
        if (id == null || id > _dataContext.TaskItems.Count())
        {
            return NotFound();
        }
        var taskFromDatabase = _dataContext.TaskItems.Find(id);
        
        return Ok(taskFromDatabase);
    }

    //Creat part
    [HttpPost("CreateTask")]
    public ActionResult<TaskItem> CreateTask(string name, string description, string selectedStatus, int priority, int projectId)
    {
        var newTask = new TaskItem(){Name = name, Description = description, ProjectId = projectId, SelectedTaskStatus = selectedStatus, Priority = priority};
        
        _dataContext.Add(newTask);
        _dataContext.SaveChanges();

        return Ok(newTask);
    }
    
    //Edit part
    [HttpPut("EditTask")]
    public ActionResult<TaskItem> Edit(int id,string name, string description, string selectedStatus, int priority, int projectId)
    {
        var editTask = new TaskItem(){Id = id, Name = name, Description = description, ProjectId = projectId, SelectedTaskStatus = selectedStatus, Priority = priority};
        
        _dataContext.Update(editTask);
        _dataContext.SaveChanges();

        return Ok(editTask);
    }
    
    //Delete part
    [HttpDelete("DeleteById")]
    public ActionResult<TaskItem> DeleteTask(int? id)
    {
        if (id == null || id > _dataContext.TaskItems.Count())
        {
            return NotFound();
        }
        var deleteTask = _dataContext.TaskItems.Find(id);
        
        _dataContext.Remove(deleteTask);
        _dataContext.SaveChanges();

        return Ok(deleteTask);
    }
}