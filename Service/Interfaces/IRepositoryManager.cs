namespace Service.Interfaces;

public interface IRepositoryManager
{
    public IUserService User { get; }
    public IProjectService Project { get; }
    public ISprintService Sprint { get; }
    public ITaskService Task { get; }
}