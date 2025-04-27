namespace StackOverflow.Entities
{
    public class QuestionComment : Comment
    {
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
    }
}
