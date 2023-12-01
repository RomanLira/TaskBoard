namespace Service.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<Data.Models.Task>> GetAllTasksAsync();
    Task<IEnumerable<Data.Models.Task>> GetAllTasksForSprintAsync(string sprintId);
    Task<IEnumerable<Data.Models.Task>> GetAllTasksForUserAsync(string userId);
    Task<Data.Models.Task?> GetTaskAsync(string id);
    Task CreateTaskAsync(Data.Models.Task task);
    Task UpdateTaskAsync(string id, Data.Models.Task task);
    Task DeleteTaskAsync(string id);
}