using Microsoft.EntityFrameworkCore;
using Wave.API.Domain.Entities;
using Wave.API.Infrastructure.Persistence.Configurations;

namespace Wave.API.Infrastructure.Persistence;

public class WaveDbContext  : DbContext
{
      public WaveDbContext(DbContextOptions<WaveDbContext> options) : base(options){ }

      public DbSet<User> User => Set<User>();

      public DbSet<Post> Post => Set<Post>();

      public DbSet<Comment> Comments => Set<Comment>();

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
          //base.OnModelCreating(modelBuilder);

          //modelBuilder.HasDefaultSchema("wave");

          modelBuilder.ApplyConfiguration(new UserConfiguration());
          modelBuilder.ApplyConfiguration(new PostConfiguration());
          modelBuilder.ApplyConfiguration(new CommentConfiguration());
      }
}
