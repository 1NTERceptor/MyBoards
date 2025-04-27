using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflow.Entities.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.CreationDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasOne(c => c.Author)
                .WithMany(q => q.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
