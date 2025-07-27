using AutoMapper;
using Azure;
using LibraryAPI.Data.Repositories;
using LibraryAPI.DTOs.Author;
using LibraryAPI.DTOs.Genre;
using LibraryAPI.Models;
using LibraryAPI.Services.Interfaces;
using Slugify;

namespace LibraryAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SlugHelper _slugHelper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper, SlugHelper slugHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _slugHelper = slugHelper;
        }

        // ------------------ CREATE ------------------
        public async Task<GenreResponseDto> CreateAsync(GenreCreateDto genreCreateDto)
        {
            var genre = new Genre
            {
                Name = genreCreateDto.Name,
                Slug = _slugHelper.GenerateSlug(genreCreateDto.Name)
            };

            await _unitOfWork.Genres.CreateAsync(genre);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GenreResponseDto>(genre);
        }

        // ------------------ DELETE ------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id)
                ?? throw new ArgumentException($"Genre with ID {id} does not exist.");

            _unitOfWork.Genres.Delete(genre);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // ------------------ GET ALL ------------------
        public async Task<List<GenreResponseDto>> GetAllAsync()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            return genres.Select(genre => _mapper.Map<GenreResponseDto>(genre)).ToList();
        }

        // ------------------ GET BY ID ------------------
        public async Task<GenreResponseDto?> GetByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id)
                ?? throw new ArgumentException($"Genre with ID {id} does not exist.");

            return _mapper.Map<GenreResponseDto>(genre);
        }

        // ------------------ GET BY SLUG ------------------

        public async Task<GenreResponseDto?> GetBySlugAsync(string slug)
        {
            var genre = await _unitOfWork.Genres.GetBySlugAsync(slug) 
                ?? throw new ArgumentException($"Genre with slug '{slug}' does not exist.");

            return _mapper.Map<GenreResponseDto>(genre);
        }

        // ------------------ GET BY NAME ------------------
        public async Task<List<GenreResponseDto>> GetByNameAsync(string name)
        {
            var genres = await _unitOfWork.Genres.GetGenresByNameAsync(name);
            return genres.Select(genre => _mapper.Map<GenreResponseDto>(genre)).ToList();
        }

        // ------------------ UPDATE ------------------
        public async Task<GenreResponseDto?> UpdateAsync(int id, GenreUpdateDto genreUpdateDto)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id)
                ?? throw new ArgumentException($"Genre with ID {id} does not exist.");

            genre.Name = genreUpdateDto.Name;
            genre.Slug = _slugHelper.GenerateSlug(genreUpdateDto.Name);

            _unitOfWork.Genres.Update(genre);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GenreResponseDto>(genre);
        }

    }
}
