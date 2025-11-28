using Microsoft.EntityFrameworkCore;
using Wave.API.Application.DTOs;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;
using Wave.API.Infrastructure.Persistence;

namespace Wave.API.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WaveDbContext _context;

    public UserRepository(WaveDbContext context) => _context = context;

    public async Task Add(User user) => await _context.User.AddAsync(user);

    public async Task<IEnumerable<User>> GetAll() => await _context.User.ToListAsync();

    public async Task<User?> GetByEmail(string email) => await _context.User.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetById(long id) => await _context.User.FirstOrDefaultAsync(u => u.Id == id);

    public async Task Update(User user)  => _context.User.Update(user);

    public async Task Delete(User user) => _context.User.Remove(user);

    public async Task SaveChanges() => await _context.SaveChangesAsync();
}
