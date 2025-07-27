using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryAPI.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name)
        {
            return await _dbSet
                .Where(author => EF.Functions.Like(author.Name, $"%{name}%"))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _dbSet
                .Include(author => author.Books)
                .ToListAsync();
        }

        public override async Task<Author?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(author => author.Books)
                .FirstOrDefaultAsync(author => author.Id == id);
        }

        public override async Task<Author?> GetBySlugAsync(string slug)
        {
            return await _dbSet
                .Include(author => author.Books)
                .FirstOrDefaultAsync(author => author.Slug == slug);
        }

        public override async Task<IEnumerable<Author>> FindAsync(Expression<Func<Author, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Include(author => author.Books)
                .ToListAsync();
        }
    }
}
