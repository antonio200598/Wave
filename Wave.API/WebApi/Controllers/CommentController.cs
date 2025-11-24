using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Wave.API.Application.DTOs;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;

namespace Wave.API.WebApi.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository) => _commentRepository = commentRepository;

    [HttpPost("create")]
    public async Task<IActionResult> CreateComment(CreateCommentRequest request)
    {
        var existingComment = await _commentRepository.GetAll();

        var newComment = new Comment
        {
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            Post = new Post { Id = request.PostId },
            User = new User { Id = request.UserId }
        };

        await _commentRepository.Add(newComment);
      
        await _commentRepository.SaveChanges();

        return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id }, newComment);
    }

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepository.GetAll();
      
        return Ok(comments);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetCommentById(long id)
    {
        var comment = await _commentRepository.GetById(id);

        if (comment == null)
          return NotFound();

        return Ok(comment);
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

        return Ok(existingComment);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteComment(long id)
    {
        var comment = await _commentRepository.GetById(id);

        if (comment == null)
          return NotFound();

        await _commentRepository.Delete(comment);

        await _commentRepository.SaveChanges();

        return NoContent();
    }
}
