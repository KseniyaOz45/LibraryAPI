using AutoMapper;
using LibraryAPI.Data.Repositories;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Models;
using LibraryAPI.Services.Interfaces;
using Slugify;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly SlugHelper _slugHelper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, SlugHelper slugHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _slugHelper = slugHelper;
        }

        // ------------------ CREATE ------------------
        public async Task<BookResponseDto> CreateAsync(BookCreateDto bookCreateDto)
        {
            // Проверка автора
            var author = await _unitOfWork.Authors.GetByIdAsync(bookCreateDto.AuthorId)
                ?? throw new ArgumentException($"Author with ID {bookCreateDto.AuthorId} does not exist.");

            // Проверка жанра
            var genre = await _unitOfWork.Genres.GetByIdAsync(bookCreateDto.GenreId)
                ?? throw new ArgumentException($"Genre with ID {bookCreateDto.GenreId} does not exist.");

            // Сохраняем обложку (если есть)
            var coverPath = await SaveCoverAsync(bookCreateDto.Cover);

            // Создаём книгу
            var book = new Book
            {
                Title = bookCreateDto.Title,
                Slug = _slugHelper.GenerateSlug(bookCreateDto.Title),
                AuthorId = bookCreateDto.AuthorId,
                GenreId = bookCreateDto.GenreId,
                CoverUrl = coverPath,
                Description = bookCreateDto.Description,
                Year = bookCreateDto.Year,
                Pages = bookCreateDto.Pages
            };

            await _unitOfWork.Books.CreateAsync(book);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<BookResponseDto>(book);
            response.AuthorName = author.Name;
            response.GenreName = genre.Name;

            return response;
        }

        // ------------------ GET ALL ------------------
        public async Task<List<BookResponseDto>> GetAllAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();

            return books.Select(book =>
            {
                var dto = _mapper.Map<BookResponseDto>(book);
                return dto;
            }).ToList();
        }

        // ------------------ SEARCH ------------------

        public async Task<List<BookResponseDto>> SearchAsync(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await GetAllAsync();
            var books = await _unitOfWork.Books.SearchBooksAsync(query);
            return books.Select(book =>
            {
                var dto = _mapper.Map<BookResponseDto>(book);
                return dto;
            }).ToList();
        }

        // ------------------ GET BY ID ------------------
        public async Task<BookResponseDto?> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id)
                ?? throw new ArgumentException($"Book with ID {id} does not exist.");

            var response = _mapper.Map<BookResponseDto>(book);
            response.AuthorName = book.Author.Name;
            response.GenreName = book.Genre.Name;

            return response;
        }

        // ------------------ GET BY SLUG ------------------

        public async Task<BookResponseDto?> GetBySlugAsync(string slug)
        {
            var book = await _unitOfWork.Books.GetBySlugAsync(slug)
                ?? throw new ArgumentException($"Book with slug '{slug}' does not exist.");

            var response = _mapper.Map<BookResponseDto>(book);
            response.AuthorName = book.Author.Name;
            response.GenreName = book.Genre.Name;
            return response;
        }

        // ------------------ UPDATE ------------------
        public async Task<BookResponseDto?> UpdateAsync(int id, BookUpdateDto bookUpdateDto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id)
                ?? throw new ArgumentException($"Book with ID {id} does not exist.");

            // Проверка автора
            if (!bookUpdateDto.AuthorId.HasValue)
                throw new ArgumentException("AuthorId is required.");

            var author = await _unitOfWork.Authors.GetByIdAsync(bookUpdateDto.AuthorId.Value)
                ?? throw new ArgumentException($"Author with ID {bookUpdateDto.AuthorId} does not exist.");

            // Проверка жанра
            if (!bookUpdateDto.GenreId.HasValue)
                throw new ArgumentException("GenreId is required.");

            var genre = await _unitOfWork.Genres.GetByIdAsync(bookUpdateDto.GenreId.Value)
                ?? throw new ArgumentException($"Genre with ID {bookUpdateDto.GenreId} does not exist.");

            // Обновляем данные
            book.Title = bookUpdateDto.Title;
            book.Year = bookUpdateDto.Year ?? book.Year;
            book.Pages = bookUpdateDto.Pages ?? book.Pages;
            book.AuthorId = bookUpdateDto.AuthorId.Value;
            book.GenreId = bookUpdateDto.GenreId.Value;

            // Обновляем slug, если название изменилось
            book.Slug = _slugHelper.GenerateSlug(bookUpdateDto.Title);

            // Если новая обложка — заменяем
            if (bookUpdateDto.Cover != null)
            {
                DeleteCoverFile(book.CoverUrl);
                book.CoverUrl = await SaveCoverAsync(bookUpdateDto.Cover);
            }

            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<BookResponseDto>(book);
            response.AuthorName = author.Name;
            response.GenreName = genre.Name;

            return response;
        }

        // ------------------ DELETE ------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return false;

            DeleteCoverFile(book.CoverUrl);

            _unitOfWork.Books.Delete(book);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        // ------------------ PRIVATE HELPERS ------------------

        private async Task<string?> SaveCoverAsync(IFormFile? cover)
        {
            if (cover == null) return null;

            var folderPath = Path.Combine(_env.WebRootPath, "images", "book_covers");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cover.CopyToAsync(stream);
            }

            // Возвращаем относительный путь (для API)
            return $"/images/book_covers/{fileName}";
        }

        private void DeleteCoverFile(string? coverUrl)
        {
            if (string.IsNullOrEmpty(coverUrl)) return;

            var path = Path.Combine(_env.WebRootPath, coverUrl.TrimStart('/'));
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
