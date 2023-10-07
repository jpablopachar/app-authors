using System.ComponentModel.DataAnnotations;

namespace app_authors.Dtos.Author
{
    public class AuthorRequestDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(5, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string? Name { get; set; }
    }
}