using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TaskBoard.Models;

namespace TaskBoard.Controllers;

[Route("api/[controller]")]
public class SprintsController : BaseController
{
    public SprintsController(IRepositoryManager repository) : base(repository)
    {
        
    }
    
    //Возвращает все спринты из базы данных
    [HttpGet]
    public async Task<IActionResult> GetAllSprints()
    {
        try
        {
            var sprints = await _repository.Sprint.GetAllSprintsAsync();
            return Ok(value: sprints);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает всех пользователей, имеющих отношение к определённому спринту
    [HttpGet("Sprint/{id}/Users")]
    public async Task<IActionResult> GetUsersForSprint(string id)
    {
        try
        {
            var users = await _repository.User.GetAllUsersForSprintAsync(id);
            return Ok(value: users);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает данные спринта по его id
    [HttpGet("Sprint/{id}")]
    public async Task<IActionResult> GetSprint(string id)
    {
        try
        {
            var sprint = await _repository.Sprint.GetSprintAsync(id);
            if (sprint == null)
            {
                return NotFound("Sprint does not found.");
            }
            return Ok(value: sprint);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает все задачи спринта
    [HttpGet("Sprint/{id}/Tasks")]
    public async Task<IActionResult> GetTasksForSprint(string id)
    {
        try
        {
            var tasks = await _repository.Task.GetAllTasksForSprintAsync(id);
            return Ok(value: tasks);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Создаёт спринт и добавляет его в базу данных
    [HttpPost]  
    public async Task<IActionResult> CreateSprint([FromBody] SprintModel model)
    {
        try
        {
            var sprint = model.Sprint;
            var users = model.Users;
            var sprints = await _repository.Sprint.GetAllSprintsAsync();
            var checkSprint = sprints.Any(s =>
                s.Name == sprint.Name &&
                s.Description == sprint.Description &&
                s.ProjectId == sprint.ProjectId);
            if (checkSprint)
                throw new Exception("This sprint already exists!");
            Guid guid = Guid.NewGuid();
            sprint.Id = TypeDescriptor.GetConverter(guid).ConvertToString(guid);
            
            await _repository.Sprint.CreateSprintAsync(sprint, users);

            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var sprintJson = JsonSerializer.Serialize(sprint, options);  
            
            return CreatedAtAction(nameof(GetSprint), new { id = sprint.Id }, sprintJson);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
    
    //Обновляет спринт
    [HttpPut]  
    public async Task<IActionResult> UpdateSprint([FromBody] SprintModel model)
    {
        try
        {
            var sprint = model.Sprint;
            var users = model.Users;
            var sprintCheck = await _repository.Sprint.GetSprintAsync(sprint.Id);
            if (sprintCheck == null)
            {
                return NotFound("Sprint does not found.");
            }
            await _repository.Sprint.UpdateSprintAsync(sprint.Id, sprint);
            await _repository.Sprint.UpdateSprintUsersAsync(sprint.Id, users);
            
            return Ok("Update done");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Удаляет спринт из базы данных
    [HttpDelete("DeleteSprint/{sprintId}")]  
    public async Task<IActionResult> DeleteSprint(string sprintId)
    {
        try
        {
            var sprint = await _repository.Sprint.GetSprintAsync(sprintId);
            if (sprint == null)
            {
                return NotFound("Sprint does not found.");
            }
            await _repository.Sprint.DeleteSprintAsync(sprintId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }  
}