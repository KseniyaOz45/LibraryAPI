using LibraryAPI.DTOs.Author;

namespace LibraryAPI.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorResponseDto> CreateAsync(AuthorCreateDto authorCreateDto);
        Task<AuthorResponseDto?> GetByIdAsync(int id);
        Task<AuthorResponseDto?> GetBySlugAsync(string slug);
        Task<List<AuthorResponseDto>> GetAllAsync();
        Task<List<AuthorResponseDto>> SearchByNameAsync(string name);
        Task<AuthorResponseDto?> UpdateAsync(int id, AuthorUpdateDto authorUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
