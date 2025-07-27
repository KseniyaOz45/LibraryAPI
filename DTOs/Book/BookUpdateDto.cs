using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs.Book
{
    public class BookUpdateDto
    {
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        [RegularExpression(@"^[\w\s\-\:\'\""\,\.\!\?]+$", ErrorMessage = "Title can only contain letters, numbers, spaces and punctuation (- : ' \" , . ! ?)")]
        [Display(Name = "Book Title")]
        public string? Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Range(1000, 9999, ErrorMessage = "Year must be a valid 4-digit year.")]
        [Display(Name = "Publication Year")]
        public int? Year { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be a positive number.")]
        [Display(Name = "Number of Pages")]
        public int? Pages { get; set; } // Number of pages in the book

        [DataType(DataType.Upload)]
        [Display(Name = "Cover Image")]
        public IFormFile? Cover { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid author.")]
        [Display(Name = "Author")]
        public int? AuthorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid genre.")]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
    }
}
