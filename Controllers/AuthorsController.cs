using LibraryAPI.DTOs.Author;
using LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorResponseDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResponseDto>> GetAuthorById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return Ok(author);
        }

        [HttpGet("by-slug/{slug}")]
        public async Task<ActionResult<AuthorResponseDto>> GetAuthorBySlug(string slug)
        {
            var author = await _authorService.GetBySlugAsync(slug);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorResponseDto>> CreateAuthor([FromForm] AuthorCreateDto authorCreateDto)
        {
            var createdAuthor = await _authorService.CreateAsync(authorCreateDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorResponseDto>> UpdateAuthor(int id, [FromForm] AuthorUpdateDto authorUpdateDto)
        {
            var updatedAuthor = await _authorService.UpdateAsync(id, authorUpdateDto);
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<AuthorResponseDto>>> SearchAuthors([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query parameter is required");

            var authors = await _authorService.SearchByNameAsync(query);
            return Ok(authors);
        }
    }
}
