namespace LibraryAPI.Data.Repositories
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IGenreRepository Genres { get; }
        IBookRepository Books { get; }
        Task<int> SaveChangesAsync();
    }
}
