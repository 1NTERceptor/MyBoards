namespace StackOverflow.Entities
{
    public class QuestionTag
    {
        public Tag Tag { get; set; }
        public Guid TagId { get; set; }
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
    }
}
