using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;
using Wave.API.Infrastructure.Persistence;

namespace Wave.API.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WaveDbContext _context;

    public UserRepository(WaveDbContext context)
    {
      _context = context;
    }

    public Task Add(User user)
    {
      throw new NotImplementedException();
    }

    public Task Delete(User user)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAll()
    {
      throw new NotImplementedException();
    }

    public Task<User?> GetByEmail(string email)
    {
      throw new NotImplementedException();
    }

    public Task<User?> GetById(long id)
    {
      throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
      throw new NotImplementedException();
    }

    public Task Update(User user)
    {
      throw new NotImplementedException();
    }
}
