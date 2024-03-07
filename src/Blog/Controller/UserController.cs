using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;


namespace projeto_final.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IBlogRepository _repository;

    public UserController(IBlogRepository repository)
    {
        _repository = repository;
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult FindUser(Guid id)
    {
        var user = _repository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        user.Password = null;
        return Ok(user);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, [FromBody] User user)
    {
        var userToUpdate = _repository.GetUserById(id);
        var token = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
        var userId = new Guid(token.Payload["UserId"].ToString()!);
        if(user == null || user.Email == null || user.Username == null || user.Password == null){
            return BadRequest();
        }
        if (userToUpdate == null)
        {
            return NotFound("User not found.");
        }
        if(userToUpdate.UserId != userId){
            return Unauthorized();
        }
        user.UserId = userToUpdate.UserId;
        
        _repository.UpdateUser(user);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        var token = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
        var userId = new Guid(token.Payload["UserId"].ToString()!);
        var userToDelete = _repository.GetUserById(id);
        if (userToDelete == null)
        {
            return NotFound("User not found.");
        }
        if(userToDelete.UserId != userId){
            return Unauthorized();
        }
        _repository.DeleteUser(userToDelete);
        return NoContent();
    }
}
