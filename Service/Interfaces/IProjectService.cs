using Data.Models;
using Task = System.Threading.Tasks.Task;

namespace Service.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<List<Project>> GetProjectsForUserAsync(string userId);
    Task<Project?> GetProjectAsync(string id);
    Task CreateProjectAsync(Project project);
    Task UpdateProjectAsync(string id, Project project);
    Task DeleteProjectAsync(string id);
}