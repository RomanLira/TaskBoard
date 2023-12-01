using System.ComponentModel;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace TaskBoard.Controllers;

[Route("api/[controller]")]
public class ProjectsController : BaseController
{
    public ProjectsController(IRepositoryManager repository) : base(repository)
    {
        
    }
    
    //Возвращает все проекты из базы данных
    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        try
        {
            var projects = await _repository.Project.GetAllProjectsAsync();
            return Ok(value: projects);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает данные проекта по его id
    [HttpGet("Project/{id}")]
    public async Task<IActionResult> GetProject(string id)
    {
        try
        {
            var project = await _repository.Project.GetProjectAsync(id);
            if (project == null)
            {
                return NotFound("Project does not found.");
            }
            return Ok(value: project);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает все спринты конкретного проекта
    [HttpGet("Project/{id}/Sprints")]
    public async Task<IActionResult> GetSprintsForProject(string id)
    {
        try
        {
            var sprints = await _repository.Sprint.GetAllSprintsForProjectAsync(id);
            return Ok(value: sprints);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Создаёт проект и добавляет его в базу данных
    [HttpPost]  
    public async Task<IActionResult> CreateProject(Project project)
    {
        try
        {
            var projects = await _repository.Project.GetAllProjectsAsync();
            var checkProject = projects.Any(p =>
                p.Name == project.Name &&
                p.Description == project.Description);
            if (checkProject)
                throw new Exception("This project already exists!");
            Guid guid = Guid.NewGuid();
            project.Id = TypeDescriptor.GetConverter(guid).ConvertToString(guid);
            await _repository.Project.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    //Обновляет проект
    [HttpPut]  
    public async Task<IActionResult> UpdateProject(Project project)
    {
        try
        {
            var projectCheck = await _repository.Project.GetProjectAsync(project.Id);
            if (projectCheck == null)
            {
                return NotFound("Project does not found.");
            }
            await _repository.Project.UpdateProjectAsync(project.Id, project);
            return Ok("Update done");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Удаляет проект из базы данных
    [HttpDelete("DeleteProject/{projectId}")]  
    public async Task<IActionResult> DeleteProject(string projectId)
    {
        try
        {
            var project = await _repository.Project.GetProjectAsync(projectId);
            if (project == null)
            {
                return NotFound("Project does not found.");
            }
            await _repository.Project.DeleteProjectAsync(projectId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }  
}