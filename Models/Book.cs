namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }

        public int Pages { get; set; } // Number of pages in the book
        public string CoverUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } // Navigation property to Author
        public int GenreId { get; set; }
        public Genre Genre { get; set; } // Navigation property to Genre

    }
}
