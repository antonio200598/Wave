using Wave.API.Domain.Entities;

namespace Wave.API.Domain.Interfaces;

public interface ICommentRepository
{
    Task Add(Comment comment);
    
    Task<Comment?> GetById(long id);

    Task Update(Comment comment);

    Task Delete(Comment comment);

    Task<IEnumerable<Comment>> GetAll();

    Task<IEnumerable<Comment>> GetByPostId(long postId);

    Task SaveChanges();

}
