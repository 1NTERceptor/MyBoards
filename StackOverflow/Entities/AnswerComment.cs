namespace StackOverflow.Entities
{
    public class AnswerComment : Comment
    {
        public Answer Answer { get; set; }
        public Guid AnswerId { get; set; }
    }
}
