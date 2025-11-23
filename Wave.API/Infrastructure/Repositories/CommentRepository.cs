using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;
using Wave.API.Infrastructure.Persistence;

namespace Wave.API.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly WaveDbContext _context;

    public CommentRepository(WaveDbContext context)
    {
        _context = context;
    }

    public Task Add(Comment comment)
    {
      throw new NotImplementedException();
    }

    public Task Delete(Comment comment)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Comment>> GetAll()
    {
      throw new NotImplementedException();
    }

    public Task<Comment?> GetById(long id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Comment>> GetByPostId(long postId)
    {
      throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
      throw new NotImplementedException();
    }

    public Task Update(Comment comment)
    {
      throw new NotImplementedException();
    }
}
