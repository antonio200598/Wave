using Microsoft.EntityFrameworkCore;
using Wave.API.Domain.Entities;

namespace Wave.API.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comment");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(x => x.Content).HasColumnName("content").IsRequired();
        builder.Property(x => x.PostId).HasColumnName("postid");
        builder.Property(x => x.UserId).HasColumnName("userid");

        builder.Property(x => x.CreatedAt)
          .HasColumnName("created_at")
          .HasColumnType("timestamp with time zone")
          .HasDefaultValueSql("NOW()");

        builder.Property(x => x.UpdatedAt)
          .HasColumnName("updated_at")
          .HasColumnType("timestamp with time zone");

        builder.HasOne(x => x.Post)
          .WithMany(p => p.Comments)
          .HasForeignKey(x => x.PostId)
          .HasConstraintName("fk_comment_post")
          .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(x => x.User)
          .WithMany(u => u.Comments)
          .HasForeignKey(x => x.UserId)
          .HasConstraintName("fk_comment_user")
          .OnDelete(DeleteBehavior.SetNull);


        builder.HasIndex(x => x.PostId).HasDatabaseName("idx_comments_post_id");
        builder.HasIndex(x => x.UserId).HasDatabaseName("idx_comments_user_id");
    }
}
