using LibraryAPI.Models;

namespace LibraryAPI.DTOs.Book
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string CoverUrl { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
