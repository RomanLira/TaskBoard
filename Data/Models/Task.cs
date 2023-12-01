using System.Text.Json.Serialization;

namespace Data.Models;

public class Task
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public string? Comment { get; set; }
    public string? Files { get; set; }
    
    [JsonIgnore]
    public User? User { get; set; }
    public string UserId { get; set; }
    
    [JsonIgnore]
    public Sprint? Sprint { get; set; }
    public string SprintId { get; set; }
}