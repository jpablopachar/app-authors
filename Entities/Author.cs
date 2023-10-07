using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app_authors.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(5, ErrorMessage ="El campo {0} no debe tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string? Name { get; set; }
        public List<AuthorBook>? BookAuthors { get; set; }
    }
}