using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;
using Wave.API.Infrastructure.Persistence;

namespace Wave.API.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly WaveDbContext _context;

    public PostRepository(WaveDbContext context)
    {
      _context = context;
    }

    public Task Add(Post post)
    {
      throw new NotImplementedException();
    }

    public Task Delete(Post post)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Post>> GetAll()
    {
      throw new NotImplementedException();
    }

    public Task<Post?> GetById(long id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Post>> GetByUserId(long userId)
    {
      throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
      throw new NotImplementedException();
    }

    public Task Update(Post post)
    {
      throw new NotImplementedException();
    }
}
