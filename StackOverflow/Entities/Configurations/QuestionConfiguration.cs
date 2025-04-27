using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace StackOverflow.Entities.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.CreationDate)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(q => q.Content)
                .IsRequired();

            builder.HasOne(q => q.Author)
                .WithMany(q => q.Questions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(q => q.Comments)
                .WithOne(c => c.Question)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(q => q.Tags)
                .WithMany(t => t.Questions)
                .UsingEntity<QuestionTag>(
                    j => j.HasOne(qt => qt.Tag)
                        .WithMany()
                        .HasForeignKey(qt => qt.TagId),
                    j => j.HasOne(qt => qt.Question)
                        .WithMany()
                        .HasForeignKey(qt => qt.QuestionId),
                    j =>
                    {
                        j.HasKey(qt => new { qt.QuestionId, qt.TagId });
                    });
        }
    }
}
