using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace TaskBoard.Controllers;

public class BaseController : Controller
{
    protected readonly IRepositoryManager _repository;

    public BaseController(IRepositoryManager repository)
    {
        _repository = repository;
    }
}