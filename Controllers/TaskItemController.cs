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

    [HttpGet("id")]
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
    [HttpPost]
    public ActionResult<TaskItem> Create(TaskItem newTask)
    {
        _dataContext.Add(newTask);
        _dataContext.SaveChanges();
            
        return Ok(newTask);
    }
    
    //Edit part
    [HttpPut]
    public ActionResult<TaskItem> Edit(TaskItem editTask)
    {
        _dataContext.Update(editTask);
        _dataContext.SaveChanges();

        return Ok(editTask);
    }
    
    //Delete part
    [HttpDelete("id")]
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