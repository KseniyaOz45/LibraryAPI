using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs.Author
{
    public class AuthorUpdateDto
    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[\p{L}\s'-]+$", ErrorMessage = "Name can only contain letters, spaces, apostrophes and hyphens.")]
        [Display(Name = "Author Name")]
        public string? Name { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot be longer than 1000 characters.")]
        [Display(Name = "Biography")]
        public string? Biography { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Author Photo")]
        public IFormFile? Photo { get; set; } // Photo file to be uploaded
    }
}
