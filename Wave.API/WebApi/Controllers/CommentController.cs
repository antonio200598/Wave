using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Wave.API.Application.DTOs;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;
using Wave.API.Infrastructure.Repositories;

namespace Wave.API.WebApi.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateComment(CreateCommentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var post = await _postRepository.GetById(request.PostId);
        if (post == null)
            return NotFound();

        var user = await _userRepository.GetById(request.UserId);
        if (user == null)
            return NotFound();

        var newComment = new Comment
        {
            Content = request.Content,
            PostId = request.PostId,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _commentRepository.Add(newComment);
      
        await _commentRepository.SaveChanges();

        return CreatedAtAction(nameof(CreateComment),new { id = newComment.Id },
        new {
            Id = newComment.Id,
            Content = newComment.Content,
            PostId = newComment.PostId,
            PostTitle = post.Title,
            PostContent = post.Content,
            UserId = newComment.UserId,
            UserName = user.Name
        });
    }

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepository.GetAll();

        var allComments = comments.Select(x => new
        {
            Id = x.Id,
            Content = x.Content,
            Post = new { Id = x.PostId },
            User = new { Id = x.UserId },
            CreationDate = x.CreatedAt,
            UpdateDate = x.UpdatedAt
        });

        return Ok(allComments);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetCommentById(long id)
    {
        var comment = await _commentRepository.GetById(id);

        if (comment == null)
          return NotFound();

        var post = await _postRepository.GetById(comment.PostId);
        
        if (post == null)
          return NotFound();

        var user = await _userRepository.GetById(comment.UserId);
        
        if (user == null)
          return NotFound();

        return Ok(new 
        { 
            Id = comment.Id, 
            Content = comment.Content, 
            UserId = comment.UserId, 
            Username = user.Name, 
            PostId = comment.PostId, 
            PostTitle = post.Title, 
            PostContent = post.Content
        });
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateComment(UpdateCommentRequest request)
    {
        var existingComment = await _commentRepository.GetById(request.CommentId);

        if (existingComment == null)
          return NotFound();

        existingComment.Content = request.Content ?? existingComment.Content;
        existingComment.UpdatedAt = DateTime.UtcNow;

        await _commentRepository.Update(existingComment);

        await _commentRepository.SaveChanges();

        return Ok("Comentário atualizado com sucesso");
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteComment(long id)
    {
        var comment = await _commentRepository.GetById(id);

        if (comment == null)
          return NotFound();

        await _commentRepository.Delete(comment);

        await _commentRepository.SaveChanges();

        return Ok("Comentário deletado com sucesso");
    }
}
