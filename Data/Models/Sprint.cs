using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models;

public class Sprint
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    
    public string? Comment { get; set; }
    public string? Files { get; set; }
    
    [JsonIgnore]
    public Project? Project { get; set; }
    public string ProjectId { get; set; }
    
    public ICollection<Task>? Tasks { get; set; }
    public ICollection<SprintUser>? SprintUsers { get; set; }
}