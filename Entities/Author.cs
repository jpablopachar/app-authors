using System.ComponentModel.DataAnnotations;
using app_authors.Validations;

namespace app_authors.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(5, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        [FirstCapitalLetter]
        public string? Name { get; set; }
        public List<AuthorBook>? AuthorBooks { get; set; }
    }
}