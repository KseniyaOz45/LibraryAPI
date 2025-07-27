using LibraryAPI.DTOs.Genre;
using LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreResponseDto>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreResponseDto>> GetGenreById(int id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            return Ok(genre);
        }

        [HttpGet("by-slug/{slug}")]
        public async Task<ActionResult<GenreResponseDto>> GetGenreBySlug(string slug)
        {
            var genre = await _genreService.GetBySlugAsync(slug);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreResponseDto>> CreateGenre([FromBody] GenreCreateDto genreCreateDto)
        {
            var createdGenre = await _genreService.CreateAsync(genreCreateDto);
            return CreatedAtAction(nameof(GetGenreById), new { id = createdGenre.Id }, createdGenre);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreResponseDto>> UpdateGenre(int id, [FromBody] GenreUpdateDto genreUpdateDto)
        {
            var updatedGenre = await _genreService.UpdateAsync(id, genreUpdateDto);
            return Ok(updatedGenre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<GenreResponseDto>>> SearchGenres([FromQuery] string name)
        {
            var genres = await _genreService.GetByNameAsync(name);
            return Ok(genres);
        }
    }
}
