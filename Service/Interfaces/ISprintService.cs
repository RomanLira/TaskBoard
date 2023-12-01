using Data.Models;
using Task = System.Threading.Tasks.Task;

namespace Service.Interfaces;

public interface ISprintService
{
    Task<IEnumerable<Sprint>> GetAllSprintsAsync();
    Task<IEnumerable<Sprint>> GetAllSprintsForProjectAsync(string projectId);
    Task<List<Sprint>> GetAllSprintsForUserAsync(string userId);
    Task<Sprint?> GetSprintAsync(string id);
    Task CreateSprintAsync(Sprint sprint, List<string> users);
    Task UpdateSprintAsync(string id, Sprint sprint);
    Task UpdateSprintUsersAsync(string sprintId, List<string> users);
    Task DeleteSprintAsync(string id);
}