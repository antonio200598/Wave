using Wave.API.Domain.Entities;

namespace Wave.API.Domain.Interfaces;

public interface IPostRepository
{
    Task Add(Post post);
    
    Task<Post?> GetById(long id);

    Task Update(Post post);

    Task Delete(Post post);

    Task<IEnumerable<Post>> GetAll();

    Task<IEnumerable<Post>> GetByUserId(long userId);

    Task SaveChanges();
}
