using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflow.Entities.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(a => a.CreationDate)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(a => a.Content)
                .IsRequired();

            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Author)                
                .WithMany(u => u.Answers)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.Comments)
                .WithOne(c => c.Answer)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
