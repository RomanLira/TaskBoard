using System.ComponentModel;
using System.Security.Claims;
using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using TaskBoard.Models;

namespace TaskBoard.Controllers;

[Route("api/[controller]")]
public class UsersController : BaseController
{
    public UsersController(IRepositoryManager repository) : base(repository)
    {
        
    }

    //Возвращает данные пользователя по его id
    [HttpGet("User/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        try
        {
            var user = await _repository.User.GetUserAsync(id);
            if (user == null)
            {
                return NotFound("User does not found.");
            }
            return Ok(value: user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    //Возвращает всех пользователей из базы данных
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _repository.User.GetAllUsersAsync();
            return Ok(value: users);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает все проекты, в которых участвует определённый пользователь
    [HttpGet("User/{id}/Projects")]
    public async Task<IActionResult> GeProjectsForUser(string id)
    {
        try
        {
            var projects = await _repository.Project.GetProjectsForUserAsync(id);
            return Ok(value: projects);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает все спринты, к которым относится определённый пользователь
    [HttpGet("User/{id}/Sprints")]
    public async Task<IActionResult> GetSprintsForUser(string id)
    {
        try
        {
            var users = await _repository.Sprint.GetAllSprintsForUserAsync(id);
            return Ok(value: users);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    //Возвращает все задачи, которые относятся к определённому пользователю
    [HttpGet("User/{id}/Tasks")]
    public async Task<IActionResult> GetTasksForUser(string id)
    {
        try
        {
            var tasks = await _repository.Task.GetAllTasksForUserAsync(id);
            return Ok(value: tasks);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    //Возвращает роль текущего пользователя
    [HttpGet("GetCurrentUserRole")]
    public async Task<IActionResult> GetCurrentUserRole()
    {
        try
        {
            var userId = Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await _repository.User.GetUserAsync(userId);
            var userRole = user?.Role.ToString();
            return Ok(new { Role = userRole });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //Возвращает id текущего пользователя
    [HttpGet("GetCurrentUserId")]
    public async Task<IActionResult> GetCurrentUserId()
    {
        try
        {
            var userId = Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await _repository.User.GetUserAsync(userId);
            var id = user?.Id;

            if (id == null)
            {
                return NotFound("User does not found.");
            }

            return Ok(new { Id = userId});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    //Возвращает логин текущего пользователя
    [HttpGet("GetCurrentUserName")]
    public async Task<IActionResult> GetCurrentUserName()
    {
        try
        {
            var userId = Convert.ToString(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var user = await _repository.User.GetUserAsync(userId);
            var login = user?.Login;

            if (login == null)
            {
                return NotFound("User does not found.");
            }

            return Ok(new { Login = login});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //Проверяет аутентифицирован ли текущий пользователь
    [HttpGet("IsAuthenticated")]
    public IActionResult IsAuthenticated()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Ok();
        }
        else
        {
            Response.Headers.Add("Cache-Control", "no-cache");
            return Unauthorized();
        }
    }

    //Создаёт пользователя и добавляет его в базу данных
    [HttpPost]  
    public async Task<IActionResult> CreateUser(User user)
    {
        try
        {
            var users = await _repository.User.GetAllUsersAsync();
            var checkUser = users.Any(u =>
                                u.Login == user.Login);
            if (checkUser)
                throw new Exception("This user already exists!");
            Guid guid = Guid.NewGuid();
            user.Id = TypeDescriptor.GetConverter(guid).ConvertToString(guid);
            user.Role = 2;
            await _repository.User.CreateUserAsync(user);
            /*var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);*/
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //Аутентификация и авторизация пользователя
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            var user = await _repository.User.GetUserByLoginAsync(model.Login);
            if (user == null || user.Password != model.Password)
            {
                return Unauthorized("Incorrect login or password! Please try again.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                
            };

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
            return Ok(new { Role = user.Role });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    //Выход пользователя из системы
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //Обновляет пользователя
    [HttpPut]  
    public async Task<IActionResult> UpdateUser(User user)
    {
        try
        {
            var userCheck = await _repository.User.GetUserAsync(user.Id);
            if (userCheck == null)
            {
                return NotFound("User does not found.");
            }
            await _repository.User.UpdateUserAsync(user.Id, user);
            return Ok("Update done");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }  
    
    //Удаляет пользователя из базы данных
    [HttpDelete("DeleteUser/{userId}")]  
    public async Task<IActionResult> DeleteUser(string userId)
    {
        try
        {
            var user = await _repository.User.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound("User does not found.");
            }
            await _repository.User.DeleteUserAsync(userId);
            return NoContent();
        }
        catch (Exception ex)
        {
           return NotFound(ex.Message);
        }
    }  
}