namespace LibraryAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name of the genre
        public string Slug { get; set; } // URL-friendly version of the genre name
        public ICollection<Book> Books { get; set; } = new List<Book>(); // Navigation property to Books
    }
}
