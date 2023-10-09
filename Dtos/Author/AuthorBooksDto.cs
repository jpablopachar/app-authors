using app_authors.Dtos.Book;

namespace app_authors.Dtos.Author
{
    public class AuthorBooksDto
    {
        public List<BookDto>? Books { get; set; }
    }
}