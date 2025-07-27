using LibraryAPI.DTOs.Book;

namespace LibraryAPI.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookResponseDto> CreateAsync(BookCreateDto bookCreateDto);
        Task<BookResponseDto?> GetByIdAsync(int id);
        Task<BookResponseDto?> GetBySlugAsync(string slug);
        Task<List<BookResponseDto>> GetAllAsync();
        Task<List<BookResponseDto>> SearchAsync(string query);
        Task<BookResponseDto?> UpdateAsync(int id, BookUpdateDto bookUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
