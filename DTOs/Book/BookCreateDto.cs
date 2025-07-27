using LibraryAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs.Book
{
    public class BookCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        [RegularExpression(@"^[\w\s\-\:\'\""\,\.\!\?]+$", ErrorMessage = "Title can only contain letters, numbers, spaces and punctuation (- : ' \" , . ! ?)")]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1000, 9999, ErrorMessage = "Year must be a valid 4-digit year.")]
        [Display(Name = "Publication Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Pages count is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Pages must be a positive number.")]
        [Display(Name = "Number of Pages")]
        public int Pages { get; set; } // Number of pages in the book

        [Required(ErrorMessage = "Cover image is required.")]
        [DataType(DataType.Upload)]
        [Display(Name = "Cover Image")]
        public IFormFile Cover { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid author.")]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid genre.")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
    }
}
