using AutoMapper;
using LibraryAPI.Data.Repositories;
using LibraryAPI.DTOs.Author;
using LibraryAPI.DTOs.Book;
using LibraryAPI.Models;
using LibraryAPI.Services.Interfaces;
using Slugify;

namespace LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly SlugHelper _slugHelper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, SlugHelper slugHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _slugHelper = slugHelper;
        }

        // ------------------ CREATE ------------------

        public async Task<AuthorResponseDto> CreateAsync(AuthorCreateDto authorCreateDto)
        {
            var avatarPath = await SaveAvatarAsync(authorCreateDto.Photo);

            var author = new Author
            {
                Name = authorCreateDto.Name,
                Slug = _slugHelper.GenerateSlug(authorCreateDto.Name),
                Biography = authorCreateDto.Biography,
                PhotoUrl = avatarPath
            };

            await _unitOfWork.Authors.CreateAsync(author);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<AuthorResponseDto>(author);
            return response;
        }

        // ------------------ DELETE ------------------

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id)
                ?? throw new ArgumentException($"Author with ID {id} does not exist.");

            // Удаляем файл аватара, если он есть
            DeleteAvatarFile(author.PhotoUrl);

            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // ------------------ GET ALL ------------------

        public async Task<List<AuthorResponseDto>> GetAllAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();

            return authors.Select(author => 
            {
                var response = _mapper.Map<AuthorResponseDto>(author);
                return response;
            }).ToList();
        }

        // ------------------ GET BY ID ------------------

        public async Task<AuthorResponseDto?> GetByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id)
                ?? throw new ArgumentException($"Author with ID {id} does not exist.");
            var response = _mapper.Map<AuthorResponseDto>(author);
            return response;
        }

        // ------------------ GET BY SLUG ------------------
        public async Task<AuthorResponseDto?> GetBySlugAsync(string slug)
        {
            var author = await _unitOfWork.Authors.GetBySlugAsync(slug)
                ?? throw new ArgumentException($"Author with slug '{slug}' does not exist.");
            var response = _mapper.Map<AuthorResponseDto>(author);
            return response;
        }

        // ------------------ SEARCH BY NAME ------------------

        public async Task<List<AuthorResponseDto>> SearchByNameAsync(string name)
        {
            var authors = await _unitOfWork.Authors.GetAuthorsByNameAsync(name);
            return authors.Select(author => 
            {
                var response = _mapper.Map<AuthorResponseDto>(author);
                return response;
            }).ToList();
        }

        // ------------------ GET AUTHORS BY NAME ------------------

        public async Task<List<AuthorResponseDto>> GetByNameAsync(string name)
        {
            var authors = await _unitOfWork.Authors.GetAuthorsByNameAsync(name);
            return authors.Select(author => 
            {
                var response = _mapper.Map<AuthorResponseDto>(author);
                return response;
            }).ToList();
        }

        // ------------------ UPDATE ------------------
        public async Task<AuthorResponseDto?> UpdateAsync(int id, AuthorUpdateDto authorUpdateDto)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id)
                ?? throw new ArgumentException($"Author with ID {id} does not exist.");

            author.Name = authorUpdateDto.Name;
            author.Biography = authorUpdateDto.Biography;

            author.Slug = _slugHelper.GenerateSlug(authorUpdateDto.Name);

            // Сохраняем новый аватар, если он есть
            if (authorUpdateDto.Photo != null)
            {
                DeleteAvatarFile(author.PhotoUrl); // Удаляем старый аватар
                author.PhotoUrl = await SaveAvatarAsync(authorUpdateDto.Photo);
            }

            _unitOfWork.Authors.Update(author);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<AuthorResponseDto>(author);
            return response;
        }

        // ------------------ PRIVATE HELPERS ------------------

        private async Task<string?> SaveAvatarAsync(IFormFile? avatar)
        {
            if (avatar == null) return null;

            var folderPath = Path.Combine(_env.WebRootPath, "images", "author_avatars");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(avatar.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(stream);
            }

            // Возвращаем относительный путь (для API)
            return $"/images/author_avatars/{fileName}";
        }

        private void DeleteAvatarFile(string? avatarUrl)
        {
            if (string.IsNullOrEmpty(avatarUrl)) return;

            var path = Path.Combine(_env.WebRootPath, avatarUrl.TrimStart('/'));
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
