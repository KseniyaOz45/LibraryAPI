namespace LibraryAPI.DTOs.Author
{
    public class AuthorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Biography { get; set; }
        public string PhotoUrl { get; set; }
    }
}
