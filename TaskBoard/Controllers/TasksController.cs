using System.ComponentModel;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Task = Data.Models.Task;

namespace TaskBoard.Controllers;

[Route("api/[controller]")]
public class TasksController : BaseController
{
    public TasksController(IRepositoryManager repository) : base(repository)
    {
        
    }
    
    //Возвращает все задачи из базы данных
    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        try
        {
            var tasks = await _repository.Task.GetAllTasksAsync();
            return Ok(value: tasks);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает данные задачи по её id
    [HttpGet("Task/{id}")]
    public async Task<IActionResult> GetTask(string id)
    {
        try
        {
            var task = await _repository.Task.GetTaskAsync(id);
            if (task == null)
            {
                return NotFound("Task does not found.");
            }
            return Ok(value: task);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Создаёт задачу и добавляет её в базу данных
    [HttpPost]  
    public async Task<IActionResult> CreateTask([FromBody] Task task)
    {
        try
        {
            var tasks = await _repository.Task.GetAllTasksAsync();
            var checkTask = tasks.Any(t =>
                t.Name == task.Name &&
                t.Description == task.Description &&
                t.SprintId == task.SprintId &&
                t.UserId == task.UserId);
            if (checkTask)
                throw new Exception("This task already exists!");
            Guid guid = Guid.NewGuid();
            task.Id = TypeDescriptor.GetConverter(guid).ConvertToString(guid);
            await _repository.Task.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    //Обновляет задачу
    [HttpPut]  
    public async Task<IActionResult> UpdateTask(Task task)
    {
        try
        {
            var taskCheck = await _repository.Task.GetTaskAsync(task.Id);
            if (taskCheck == null)
            {
                return NotFound("Task does not found.");
            }
            await _repository.Task.UpdateTaskAsync(task.Id, task);
            return Ok("Update done");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Удаляет задачу из базы данных
    [HttpDelete("DeleteTask/{taskId}")]  
    public async Task<IActionResult> DeleteTask(string taskId)
    {
        try
        {
            var task = await _repository.Task.GetTaskAsync(taskId);
            if (task == null)
            {
                return NotFound("Task does not found.");
            }
            await _repository.Task.DeleteTaskAsync(taskId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }  
}