using Microsoft.EntityFrameworkCore;
using Wave.API.Infrastructure.Persistence;
using Wave.API.Infrastructure.Repositories;

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

      builder.Services.AddScoped<Domain.Interfaces.IUserRepository,UserRepository>();
      builder.Services.AddScoped<Domain.Interfaces.IPostRepository,PostRepository>();
      builder.Services.AddScoped<Domain.Interfaces.ICommentRepository,CommentRepository>();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
