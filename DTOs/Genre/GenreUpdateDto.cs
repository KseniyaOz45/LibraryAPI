using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs.Genre
{
    public class GenreUpdateDto
    {
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-']+$", ErrorMessage = "Name can only contain letters, numbers, spaces, hyphens, and apostrophes.")]
        [Display(Name = "Genre Name")]
        public string? Name { get; set; } // Name of the genre
    }
}
