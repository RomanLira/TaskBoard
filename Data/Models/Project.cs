namespace Data.Models;

public class Project
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Sprint>? Sprints { get; set; }
}