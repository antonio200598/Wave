using Microsoft.EntityFrameworkCore;
using Wave.API.Application.Services;
using Wave.API.Infrastructure.Persistence;
using Wave.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Adicione esta linha no topo do arquivo
using Microsoft.IdentityModel.Tokens; // Adicione esta linha no topo do arquivo
using System.Text; // Adicione esta linha no topo do arquivo



namespace Wave.API.WebApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

      builder.Services.AddDbContext<WaveDbContext>(o => o.UseNpgsql(connectionString));

      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddScoped<Domain.Interfaces.IUserRepository, UserRepository>();
      builder.Services.AddScoped<Domain.Interfaces.IPostRepository, PostRepository>();
      builder.Services.AddScoped<Domain.Interfaces.ICommentRepository, CommentRepository>();
      builder.Services.AddScoped<Domain.Interfaces.IUserService, UserService>();

      builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
      {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;

          options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
          {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])
              ),
          };
      });

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}
