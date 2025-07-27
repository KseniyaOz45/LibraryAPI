using LibraryAPI.DTOs.Book;
using LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookResponseDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseDto>> GetBookById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("by-slug/{slug}")]
        public async Task<ActionResult<BookResponseDto>> GetBookBySlug(string slug)
        {
            var book = await _bookService.GetBySlugAsync(slug);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookResponseDto>> CreateBook([FromForm] BookCreateDto bookCreateDto)
        {
            var createdBook = await _bookService.CreateAsync(bookCreateDto);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookResponseDto>> UpdateBook(int id, [FromForm] BookUpdateDto bookUpdateDto)
        {
            var updatedBook = await _bookService.UpdateAsync(id, bookUpdateDto);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<BookResponseDto>>> SearchBooks([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query parameter is required");

            var books = await _bookService.SearchAsync(query);
            return Ok(books);
        }

    }
}
