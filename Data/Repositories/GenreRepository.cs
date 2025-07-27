using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryAPI.Data.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(LibraryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Genre>> GetGenresByNameAsync(string name)
        {
            return await _dbSet
                .Where(genre => EF.Functions.Like(genre.Name, $"%{name}%"))
                .Include(genre => genre.Books)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _dbSet
                .Include(genre => genre.Books)
                .ToListAsync();
        }

        public override async Task<Genre?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(genre => genre.Books)
                .FirstOrDefaultAsync(genre => genre.Id == id);
        }

        public override async Task<Genre?> GetBySlugAsync(string slug)
        {
            return await _dbSet
                .Include(genre => genre.Books)
                .FirstOrDefaultAsync(genre => genre.Slug == slug);
        }
        public override async Task<IEnumerable<Genre>> FindAsync(Expression<Func<Genre, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Include(genre => genre.Books)
                .ToListAsync();
        }
    }
}
