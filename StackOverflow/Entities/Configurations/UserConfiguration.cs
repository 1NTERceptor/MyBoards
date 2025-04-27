using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflow.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.CreationDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(u => u.Questions)
                .WithOne(q => q.Author)
                .HasForeignKey(q => q.AuthorId);

            builder.HasMany(u => u.Answers)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);
        }
    }
}
