using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;
using Wave.API.Infrastructure.Persistence;

namespace Wave.API.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly WaveDbContext _context;

    public PostRepository(WaveDbContext context) => _context = context;

    public async Task Add(Post post) => await _context.Post.AddAsync(post);

    public async Task<IEnumerable<Post>> GetAll() => await _context.Post.ToListAsync();

    public async Task<Post?> GetById(long id) => await _context.Post.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Post>> GetByUserId(long userId) => await _context.Post.Where(p => p.UserId == userId).ToListAsync();

    public async Task Update(Post post) => _context.Post.Update(post);

    public async Task Delete(Post post) => _context.Post.Remove(post);

    public async Task SaveChanges() => await _context.SaveChangesAsync();
}
