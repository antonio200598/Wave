using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wave.API.Application.DTOs;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;

namespace Wave.API.WebApi.Controllers;

[Route("api/post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository) => _postRepository = postRepository;

    [HttpPost("create")]
    public async Task<IActionResult> CreatePost(CreatePostRequest request)
    {
        var newPost = new Post
        {
            Title = request.Title,
            Content = request.Content,
            User = new User { Id = request.UserId }
        };

        return Ok();    
    }

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAllPosts()
    { 
        var posts = await _postRepository.GetAll();
      
        return Ok(posts);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdatePost(UpdatePostRequest request)
    {
        var post = await _postRepository.GetById(request.Id);

        if (post == null)
            return NotFound();

        if(!string.IsNullOrEmpty(request.Title))
          post.Title = request.Title;

        if(!string.IsNullOrEmpty(request.Content))
          post.Content = request.Content;

        await _postRepository.Update(post);

        await _postRepository.SaveChanges();
        
        return Ok(post);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeletePost(long id)
    {
        var post = await _postRepository.GetById(id);

        if (post == null)
            return NotFound();

        await _postRepository.Delete(post);

        await _postRepository.SaveChanges();

        return NoContent();
    }
}
