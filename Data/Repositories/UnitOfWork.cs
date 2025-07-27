
namespace LibraryAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;

        public IAuthorRepository Authors { get; }
        public IGenreRepository Genres { get; }
        public IBookRepository Books { get; }

        public UnitOfWork(LibraryContext context, 
            IAuthorRepository authorRepository, 
            IGenreRepository genreRepository, 
            IBookRepository bookRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Authors = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            Genres = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            Books = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
