using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Service.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Service.Classes;

public class SprintService : Repository<Sprint>, ISprintService
{
    public SprintService(ApplicationContext applicationContext) : base(applicationContext)
    {
        
    }

    public async Task<IEnumerable<Sprint>> GetAllSprintsAsync() =>
        await GetAllAsync().Result.OrderBy(sprint => sprint.Name).ToListAsync();
    
    public async Task<IEnumerable<Sprint>> GetAllSprintsForProjectAsync(string projectId) =>
        await GetAllAsync().Result.Where(sprint => sprint.ProjectId.Equals(projectId)).OrderBy(sprint => sprint.StartDate).ToListAsync();
    
    public async Task<List<Sprint>> GetAllSprintsForUserAsync(string userId)
    {
        var sprints = new List<Sprint>();
        var sprintIds = await _applicationContext.SprintUsers.Where(sprintUser => sprintUser.UserId == userId)
            .Select(sprintUser => sprintUser.SprintId).ToListAsync();
        foreach (var id in sprintIds)
        {
            var sprint = GetSprintAsync(id);
            sprints.Add(await sprint);
        }
        return sprints;
    }

    public async Task<Sprint?> GetSprintAsync(string id) =>
        await GetAsync(sprint => sprint.Id.Equals(id)).Result.SingleOrDefaultAsync();

    public async Task CreateSprintAsync(Sprint sprint, List<string> users)
    {
        await CreateAsync(sprint);
        await SaveChangesAsync();

        foreach (var user in users)
        {
            var sprintUser = new SprintUser { SprintId = sprint.Id, UserId = user };
            _applicationContext.SprintUsers.Add(sprintUser);
        }

        await SaveChangesAsync();
    }

    public async Task UpdateSprintAsync(string id, Sprint sprint)
    {
        var changedSprint = await GetSprintAsync(id);
        if (changedSprint != null)
        {
            changedSprint.Name = sprint.Name;
            changedSprint.Description = sprint.Description;
            changedSprint.StartDate = sprint.StartDate;
            changedSprint.EndDate = sprint.EndDate;
            changedSprint.Comment = sprint.Comment;
            changedSprint.Files = sprint.Files;
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("Sprint does not found.");
        }
    }
    
    public async Task UpdateSprintUsersAsync(string sprintId, List<string> users)
    {
        var sprintUsers = await _applicationContext.SprintUsers.Where(sprintUser => sprintUser.SprintId == sprintId).ToListAsync();
        var usersToDelete = sprintUsers.Where(sprintUser => !users.Contains(sprintUser.UserId))
            .Select(sprintUser => sprintUser.UserId).ToList();
        
        foreach (var userToDelete in usersToDelete)
        {
            var sprintUserToDelete = sprintUsers.FirstOrDefault(sprintUser => sprintUser.UserId == userToDelete);
            if (sprintUserToDelete != null)
                _applicationContext.SprintUsers.Remove(sprintUserToDelete);
        }

        foreach (var user in users)
        {
            if (!sprintUsers.Any(sprintUser => sprintUser.UserId == user))
            {
                var sprintUser = new SprintUser { SprintId = sprintId, UserId = user };
                _applicationContext.SprintUsers.Add(sprintUser);
            }
        }

        await _applicationContext.SaveChangesAsync();
    }
    
    public async Task DeleteSprintAsync(string id)
    {
        var sprint = await GetSprintAsync(id);
        if (sprint != null)
        {
            await DeleteAsync(sprint);
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("Sprint does not found.");
        }
    }
}