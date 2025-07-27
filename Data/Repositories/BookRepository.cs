using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryAPI.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string authorName)
        {
            return await _dbSet
                .Where(book => EF.Functions.Like(book.Author.Name, $"%{authorName}%"))
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(string genre)
        {
            return await _dbSet
                .Where(book => EF.Functions.Like(book.Genre.Name, $"%{genre}%"))
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByPublicationYearAsync(int year)
        {
            return await _dbSet
                .Where(book => book.Year == year)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await FindAsync(book => EF.Functions.Like(book.Title, $"%{searchTerm}%") ||
                                           EF.Functions.Like(book.Author.Name, $"%{searchTerm}%") ||
                                           EF.Functions.Like(book.Genre.Name, $"%{searchTerm}%"));
        }
        public async Task<int> GetTotalBooksCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbSet
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .ToListAsync();
        }

        public override async Task<Book?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .FirstOrDefaultAsync(book => book.Id == id);
        }

        public override async Task<Book?> GetBySlugAsync(string slug)
        {
            return await _dbSet
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .FirstOrDefaultAsync(book => book.Slug == slug);
        }

        public override async Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .Include(book => book.Author)
                .Include(book => book.Genre)
                .ToListAsync();
        }
    }
}
