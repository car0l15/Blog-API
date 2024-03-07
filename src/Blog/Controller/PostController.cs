using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;


namespace projeto_final.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IBlogRepository _repository;

    public PostController(IBlogRepository repository)
    {
        _repository = repository;
    }

    [Authorize]
    [HttpGet("{postId}")]
    public IActionResult GetPost(Guid postId)
    {
        var post = _repository.GetPost(postId);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public IActionResult GetAll(Guid userId)
    {
       return Ok(_repository.GetPostsByUser(userId));
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreatePost([FromBody] Post post)
    {
        var token = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
        var userId = new Guid(token.Payload["UserId"].ToString()!);
        if(post == null){
            return BadRequest();
        }
        post.UserId = userId;
        post.PostId = Guid.NewGuid();
        post.CreatedAt = DateTimeOffset.Now;
        post.LastModified = DateTimeOffset.Now;
        _repository.CreatePost(post);
        return CreatedAtAction("CreatePost", post);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdatePost(Guid id, [FromBody] Post post)
    {
        var token = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
        var userId = new Guid(token.Payload["UserId"].ToString()!);
        var postToUpdate = _repository.GetPost(id);
        if(post == null){
            return BadRequest();
        }
        if (postToUpdate == null)
        {
            return NotFound("Post not found.");
        }
        if(postToUpdate.UserId != userId){
            return Unauthorized();
        }
        post.CreatedAt = postToUpdate.CreatedAt;
        post.LastModified = DateTimeOffset.Now;
        post.UserId = userId;
        post.PostId = id;
        _repository.UpdatePost(id, post);
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeletePost(Guid id)
    {
        var token = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
        var userId = new Guid(token.Payload["UserId"].ToString()!);
        var postToDelete = _repository.GetPost(id);
        if (postToDelete == null)
        {
            return NotFound();
        }
        if(postToDelete.UserId != userId){
            return Unauthorized();
        }
        _repository.DeletePost(id);
        return NoContent();
    }
}
