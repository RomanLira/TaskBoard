using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Service.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Service.Classes;

public class TaskService : Repository<Data.Models.Task>, ITaskService
{
    public TaskService(ApplicationContext applicationContext) : base(applicationContext)
    {
        
    }

    public async Task<IEnumerable<Data.Models.Task>> GetAllTasksAsync() =>
        await GetAllAsync().Result.OrderBy(task => task.Name).ToListAsync();
    
    public async Task<IEnumerable<Data.Models.Task>> GetAllTasksForSprintAsync(string sprintId) =>
        await GetAllAsync().Result.Where(task => task.SprintId.Equals(sprintId)).OrderBy(task => task.Status).ToListAsync();
    
    public async Task<IEnumerable<Data.Models.Task>> GetAllTasksForUserAsync(string userId) =>
        await GetAllAsync().Result.Where(task => task.UserId.Equals(userId)).OrderBy(task => task.Status).ToListAsync();

    public async Task<Data.Models.Task?> GetTaskAsync(string id) =>
        await GetAsync(task => task.Id.Equals(id)).Result.SingleOrDefaultAsync();

    public async Task CreateTaskAsync(Data.Models.Task task)
    {
        await CreateAsync(task);
        await SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(string id, Data.Models.Task task)
    {
        var changedTask = await GetTaskAsync(id);
        if (changedTask != null)
        {
            changedTask.Name = task.Name;
            changedTask.Description = task.Description;
            changedTask.Status = task.Status;
            changedTask.Comment = task.Comment;
            changedTask.Files = task.Files;
            changedTask.UserId = task.UserId;
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("Task does not found.");
        }
    }


    public async Task DeleteTaskAsync(string id)
    {
        var task = await GetTaskAsync(id);
        if (task != null)
        {
            await DeleteAsync(task);
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("Task does not found.");
        }
    }
}