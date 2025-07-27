using LibraryAPI.Models;

namespace LibraryAPI.Data.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name);
    }
}
