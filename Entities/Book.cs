namespace app_authors.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<AuthorBook>? BookAuthors { get; set; }
    }
}