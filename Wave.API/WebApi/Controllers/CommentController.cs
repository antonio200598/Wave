using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Wave.API.Application.DTOs;
using Wave.API.Domain.Interfaces;

namespace Wave.API.WebApi.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository) => _commentRepository = commentRepository;

    [HttpGet("/all")]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepository.GetAll();
      
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById(long id)
    {
        var comment = await _commentRepository.GetById(id);

        if (comment == null)
          return NotFound();

        return Ok(comment);
    }

    public async Task<IActionResult> GetCommentsByPostId(long postId)
    {
        var comments = await _commentRepository.GetByPostId(postId);

        if (comments == null || comments.Count() == 0)
          return NotFound("Não existe comentario para essa postagem");

        return Ok(comments);
    }

    public async Task<IActionResult> DeleteComment(long id)
    {
        var comment = await _commentRepository.GetById(id);

        if (comment == null)
          return NotFound();

        await _commentRepository.Delete(comment);

        await _commentRepository.SaveChanges();

        return NoContent();
    }

    public async Task<IActionResult> UpdateComment(UpdateCommentRequest request)
    {      

        var existingComment = await _commentRepository.GetById(request.CommentId);

        if (existingComment == null)
            return NotFound();

        existingComment.UpdatedAt = DateTime.UtcNow;
      
        await _commentRepository.Update(existingComment);
      
        await _commentRepository.SaveChanges();

        return Ok(existingComment);
    }
}
