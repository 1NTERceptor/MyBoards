namespace StackOverflow.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public User Author { get; set; }

        public Guid AuthorId { get; set; }

        public Question Question { get; set; } 

        public Guid QuestionId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<AnswerComment> Comments { get; set; } = new List<AnswerComment>();

        public int Rate { get; set; }
    }
}
