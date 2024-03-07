using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;
using projeto_final.Services;
using Microsoft.AspNetCore.Authorization;


namespace projeto_final.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IBlogRepository _repository;

    public AuthenticationController(IBlogRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        var userToLogin = _repository.GetUserByEmail(user.Email);
        if (userToLogin == null)
        {
            return NotFound("User not Found");
        }
        if (userToLogin.Password != user.Password)
        {
            return BadRequest("Invalid credentials");
        }
        userToLogin.Password = null;
        var token = new TokenGenerator().Generate(userToLogin);
        return Ok(token);
    }
   

    [AllowAnonymous]
    [HttpPost("signup")]
    public IActionResult Signup([FromBody] User user)
    {
        var userToSignup = _repository.GetUserByEmail(user.Email);
        if (userToSignup != null)
        {
          return BadRequest("User already exists");
        } if(user.Username == null || user.Password == null)
        {
          return BadRequest("Username and password are required");
        }
        user.UserId = Guid.NewGuid();

        _repository.CreateUser(user);
        user.Password = null;
        var token = new TokenGenerator().Generate(user);
        
        return CreatedAtAction("Signup", token);
    }
    
}
