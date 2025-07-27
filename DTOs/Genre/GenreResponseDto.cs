namespace LibraryAPI.DTOs.Genre
{
    public class GenreResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // Name of the genre
        public string Slug { get; set; } // URL-friendly version of the genre name
        public ICollection<string> Books { get; set; } = new List<string>();
    }
}
