using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		private readonly ILibraryServices _libraryServices;

		public BookController(ILibraryServices libraryServices)
		{
			_libraryServices = libraryServices;
		}

		[HttpGet]
		public async Task<IActionResult> GetBooks()
		{
			var books = await _libraryServices.GetBooksAsync();
			if (books == null)
			{
				return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
			}

			return StatusCode(StatusCodes.Status200OK, books);
		}



		[HttpGet("{id}")]
		public async Task<IActionResult> GetBooks(int id)
		{
			Book book = await _libraryServices.GetBookAsync(id);
			
			if (book == null)
			{
				return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {id}");
			}

			return StatusCode(StatusCodes.Status200OK, book);
		}

		[HttpPost]
		public async Task<ActionResult<Book>> AddBook(Book book)
		{
			var dbBook = await _libraryServices.AddBookAsync(book);
			
			if (dbBook == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"{book.Title} could not be added.");
			}

			return CreatedAtAction("GetBook", new { id = book.Id }, book);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, Book book)
		{
			if (id != book.Id)
			{
				return BadRequest();
			}

			var updatedBook = await _libraryServices.UpdateBookAsync(book);

			if (updatedBook == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"{book.Title} could not be updated.");
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var book = await _libraryServices.GetBookAsync(id);
			if (book == null)
			{
				return NotFound($"No book found for id: {id}");
			}

			var (status, message) = await _libraryServices.DeleteBookAsync(book);

			if (!status)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, message);
			}

			return Ok(book);
		}
	}
}
