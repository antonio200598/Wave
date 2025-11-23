using Microsoft.EntityFrameworkCore;
using Wave.API.Domain.Entities;

namespace Wave.API.Infrastructure.Persistence;

public class WaveDbContext  : DbContext
{
      public WaveDbContext(DbContextOptions<WaveDbContext> options) : base(options){ }

      public DbSet<User> Users => Set<User>();

      public DbSet<Post> Posts => Set<Post>();

      public DbSet<Comment> Comments => Set<Comment>();

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
          base.OnModelCreating(modelBuilder);

          modelBuilder.HasDefaultSchema("wave");

          modelBuilder.Entity<User>(entity =>
          {
              entity.HasKey(e => e.Id);
              entity.HasMany(e => e.Posts).WithOne(p => p.User);
              entity.HasMany(e => e.Comments).WithOne(c => c.User);
          });
          modelBuilder.Entity<Post>(entity =>
          {
              entity.HasKey(e => e.Id);
              entity.HasMany(e => e.Comments).WithOne(c => c.Post);
          });
          modelBuilder.Entity<Comment>(entity =>
          {
              entity.HasKey(e => e.Id);
          });
  }

}
