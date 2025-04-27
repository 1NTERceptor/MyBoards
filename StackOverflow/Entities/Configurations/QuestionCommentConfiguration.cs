using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflow.Entities.Configurations
{
    public class QuestionCommentConfiguration : IEntityTypeConfiguration<QuestionComment>
    {
        public void Configure(EntityTypeBuilder<QuestionComment> builder)
        {
            builder.HasOne(qc => qc.Question)
                .WithMany(q => q.Comments)
                .HasForeignKey(qc => qc.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
