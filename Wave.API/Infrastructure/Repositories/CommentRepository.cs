using Microsoft.EntityFrameworkCore;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;
using Wave.API.Infrastructure.Persistence;

namespace Wave.API.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly WaveDbContext _context;

    public CommentRepository(WaveDbContext context) => _context = context;

    public async Task Add(Comment comment) => await _context.Comments.AddAsync(comment);

    public async Task<IEnumerable<Comment>> GetAll() => await _context.Comments.ToListAsync();

    public async Task<Comment?> GetById(long id) => await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Comment>> GetByPostId(long postId) => await _context.Comments.Where(c => c.Post.Id == postId).ToListAsync();

    public async Task Update(Comment comment) => _context.Comments.Update(comment);

    public async Task Delete(Comment comment) => _context.Comments.Remove(comment);

    public async Task SaveChanges() => await _context.SaveChangesAsync();
}
