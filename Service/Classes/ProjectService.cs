using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Service.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Service.Classes;

public class ProjectService : Repository<Project>, IProjectService
{
    public ProjectService(ApplicationContext applicationContext) : base(applicationContext)
    {
        
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync() =>
        await GetAllAsync().Result.OrderBy(project => project.Name).ToListAsync();

    public async Task<List<Project>> GetProjectsForUserAsync(string userId)
    {
        var userProjects = new List<Project>();
        var userSprints = await _applicationContext.SprintUsers.Where(sprintUser => sprintUser.UserId == userId).Select(sprintUser => sprintUser.SprintId).ToListAsync();
        
        foreach (var userSprint in userSprints)
        {
            var sprint = _applicationContext.Sprints.FirstOrDefault(sprint => sprint.Id == userSprint);
            var project = _applicationContext.Projects.FirstOrDefault(project => project.Id == sprint.ProjectId);
            if (!userProjects.Contains(project))
            {
                userProjects.Add(project);
            }
        }
        return userProjects;
    }

    public async Task<Project?> GetProjectAsync(string id) =>
        await GetAsync(project => project.Id.Equals(id)).Result.SingleOrDefaultAsync();

    public async Task CreateProjectAsync(Project project)
    {
        await CreateAsync(project);
        await SaveChangesAsync();
    }

    public async Task UpdateProjectAsync(string id, Project project)
    {
        var changedProject = await GetProjectAsync(id);
        if (changedProject != null)
        {
            changedProject.Name = project.Name;
            changedProject.Description = project.Description;
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("Project does not found.");
        }
    }


    public async Task DeleteProjectAsync(string id)
    {
        var project = await GetProjectAsync(id);
        if (project != null)
        {
            await DeleteAsync(project);
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("Project does not found.");
        }
    }
}