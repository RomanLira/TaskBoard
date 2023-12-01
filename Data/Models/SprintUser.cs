namespace Data.Models;

public class SprintUser
{
    public string SprintId { get; set; }
    public Sprint? Sprint { get; set; }
    
    public string UserId { get; set; }
    public User? User { get; set; }
}