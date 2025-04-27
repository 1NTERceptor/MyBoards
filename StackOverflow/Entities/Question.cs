using Microsoft.EntityFrameworkCore;

namespace StackOverflow.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public User Author { get; set; }

        public Guid AuthorId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<QuestionComment> Comments { get; set; } = new List<QuestionComment>();

        public List<Answer> Answers { get; set; } = new List<Answer>();

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
