using Data.Models;
using Task = System.Threading.Tasks.Task;

namespace Service.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<List<User>> GetAllUsersForSprintAsync(string sprintId);
    Task<User?> GetUserAsync(string id);
    Task<User?> GetUserByLoginAsync(string login);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(string id, User user);
    Task DeleteUserAsync(string id);
}