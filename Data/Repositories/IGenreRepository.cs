using LibraryAPI.Models;

namespace LibraryAPI.Data.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetGenresByNameAsync(string name);
    }
}
