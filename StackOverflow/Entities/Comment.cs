namespace StackOverflow.Entities
{
    public abstract class Comment
    {
        public Guid Id { get; set; }
        public User Author { get; set; }

        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
