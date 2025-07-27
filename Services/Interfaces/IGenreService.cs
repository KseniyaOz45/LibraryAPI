using LibraryAPI.DTOs.Genre;

namespace LibraryAPI.Services.Interfaces
{
    public interface IGenreService
    {
        Task<GenreResponseDto> CreateAsync(GenreCreateDto genreCreateDto);
        Task<GenreResponseDto?> GetByIdAsync(int id);
        Task<GenreResponseDto?> GetBySlugAsync(string slug);
        Task<List<GenreResponseDto>> GetByNameAsync(string name);
        Task<List<GenreResponseDto>> GetAllAsync();
        Task<GenreResponseDto?> UpdateAsync(int id, GenreUpdateDto genreUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
