using System.ComponentModel.DataAnnotations;

namespace app_authors.Dtos
{
    public class EditAdminDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}