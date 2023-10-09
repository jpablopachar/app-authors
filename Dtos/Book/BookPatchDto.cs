using System.ComponentModel.DataAnnotations;
using app_authors.Validations;

namespace app_authors.Dtos.Book
{
    public class BookPatchDto
    {
        [FirstCapitalLetter]
        [StringLength(maximumLength:250)]
        public string? Title { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}