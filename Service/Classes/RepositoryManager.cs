using Repository.Classes;
using Service.Interfaces;

namespace Service.Classes;

public class RepositoryManager : IRepositoryManager
{
    private ApplicationContext _applicationContext;

    private IUserService _userService;
    private IProjectService _projectService;
    private ISprintService _sprintService;
    private ITaskService _taskService;

    public RepositoryManager(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public IUserService User
    {
        get
        {
            if (_userService is null)
            {
                _userService = new UserService(_applicationContext);
            }

            return _userService;
        }
    }

    public IProjectService Project
    {
        get
        {
            if (_projectService is null)
            {
                _projectService = new ProjectService(_applicationContext);
            }

            return _projectService;
        }
    }

    public ISprintService Sprint
    {
        get
        {
            if (_sprintService is null)
            {
                _sprintService = new SprintService(_applicationContext);
            }

            return _sprintService;
        }
    }

    public ITaskService Task
    {
        get
        {
            if (_taskService is null)
            {
                _taskService = new TaskService(_applicationContext);
            }

            return _taskService;
        }
    }
}