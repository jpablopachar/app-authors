using System.ComponentModel.DataAnnotations;
using app_authors.Validations;

namespace app_authors.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [FirstCapitalLetter]
        [StringLength(maximumLength:250)]
        public string? Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<AuthorBook>? BookAuthors { get; set; }
    }
}