namespace StackOverflow.DTO
{
    public class AnswerModel
    {
        public string Content { get; set; }

        public Guid AuthorId { get; set; }

        public Guid QuestionId { get; set; }

        public int Rate { get; set; }
    }
}
