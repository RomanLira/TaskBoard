using System.ComponentModel;

namespace Data.Models;

public class User
{
    public string Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
    
    public ICollection<SprintUser>? SprintUsers { get; set; }
    public ICollection<Task>? Tasks { get; set; }
}