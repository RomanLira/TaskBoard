using Data.Models;

namespace TaskBoard.Models;

public class SprintModel
{
    public Sprint Sprint { get; set; }
    public List<string> Users { get; set; }
}