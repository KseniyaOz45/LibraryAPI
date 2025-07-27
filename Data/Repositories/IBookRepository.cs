using LibraryAPI.Models;

namespace LibraryAPI.Data.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string authorName);
        Task<IEnumerable<Book>> GetBooksByGenreAsync(string genre);
        Task<IEnumerable<Book>> GetBooksByPublicationYearAsync(int year);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<int> GetTotalBooksCountAsync();
    }
}
