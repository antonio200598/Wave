using System.Runtime.CompilerServices;
using Wave.API.Domain.Entities;

namespace Wave.API.Domain.Interfaces;

public interface IUserRepository
{
    Task Add(User user);
    
    Task<User?> GetById(long id);

    Task<User?> GetByEmail(string email);

    Task Update(User user);

    Task Delete(User user);

    Task<IEnumerable<User>> GetAll();

    Task SaveChanges();
}
