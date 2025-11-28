using System.Security.Claims;
using System.Text;
using Wave.API.Domain.Entities;

namespace Wave.API.Domain.Interfaces;

public interface  IUserService
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string hash);

    string GenerateJwtToken(User user);
}
