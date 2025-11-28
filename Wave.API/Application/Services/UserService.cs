using crypto;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wave.API.Domain.Entities;
using Wave.API.Domain.Interfaces;


namespace Wave.API.Application.Services;

public class UserService : IUserService
{

    public readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        var secret = _configuration["Jwt:Secret"];
        var key = Encoding.ASCII.GetBytes(secret);

        var claims = new[]
        {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Type.ToString())
                };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.UtcNow.AddHours(4),
          SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string HashPassword(string password)
    {
        var passwordBytes = BCrypt.PasswordToByteArray(password.ToCharArray());

        var salt = new byte[16];

        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            rng.GetBytes(salt);
        
        var hashBytes = BCrypt.Generate(passwordBytes, salt, 10);
        var result = new byte[salt.Length + hashBytes.Length];

        Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
        Buffer.BlockCopy(hashBytes, 0, result, salt.Length, hashBytes.Length);

        return Convert.ToBase64String(result);
    }

    public bool VerifyPassword(string password, string hash)
    {
        var hashBytes = Convert.FromBase64String(hash);

        var salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

        var storedHash = new byte[hashBytes.Length - 16];
        Buffer.BlockCopy(hashBytes, 16, storedHash, 0, storedHash.Length);

        var passwordBytes = BCrypt.PasswordToByteArray(password.ToCharArray());
        var computedHash = BCrypt.Generate(passwordBytes, salt, 10);

        return storedHash.SequenceEqual(computedHash);
    }
}
