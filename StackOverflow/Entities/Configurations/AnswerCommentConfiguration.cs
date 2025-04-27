using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StackOverflow.Entities.Configurations
{
    public class AnswerCommentConfiguration : IEntityTypeConfiguration<AnswerComment>
    {
        public void Configure(EntityTypeBuilder<AnswerComment> builder)
        {
            builder.HasOne(ac => ac.Answer)
                .WithMany(a => a.Comments)
                .HasForeignKey(ac => ac.AnswerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
