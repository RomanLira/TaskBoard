using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Service.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Service.Classes;

public class UserService : Repository<User>, IUserService
{
    public UserService(ApplicationContext applicationContext) : base(applicationContext)
    {
        
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() =>
        await GetAllAsync().Result.OrderBy(user => user.Login).ToListAsync();

    public async Task<List<User>> GetAllUsersForSprintAsync(string sprintId)
    {
        var users = new List<User>();
        var userIds = await _applicationContext.SprintUsers.Where(sprintUser => sprintUser.SprintId == sprintId)
            .Select(sprintUser => sprintUser.UserId).ToListAsync();
        foreach (var id in userIds)
        {
            var user = GetUserAsync(id);
            users.Add(await user);
        }
        return users;
    }

    public async Task<User?> GetUserAsync(string id) =>
        await GetAsync(user => user.Id.Equals(id)).Result.SingleOrDefaultAsync();

    public async Task<User?> GetUserByLoginAsync(string login) =>
        await GetAsync(user => user.Login.Equals(login)).Result.SingleOrDefaultAsync();

    public async Task CreateUserAsync(User user)
    {
        await CreateAsync(user);
        await SaveChangesAsync();
    }

    public async Task UpdateUserAsync(string id, User user)
    {
        var changedUser = await GetUserAsync(id);
        if (changedUser != null)
        {
            changedUser.Login = user.Login;
            changedUser.Password = user.Password;
            changedUser.Role = user.Role;
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("User does not found.");
        }
    }


    public async Task DeleteUserAsync(string id)
    {
        var user = await GetUserAsync(id);
        if (user != null)
        {
            await DeleteAsync(user);
            await SaveChangesAsync();
        }
        else
        {
            throw new Exception("User does not found.");
        }
    }
}