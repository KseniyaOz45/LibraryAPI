namespace LibraryAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Biography { get; set; }
        public string PhotoUrl { get; set; }
        // Navigation property to books
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
