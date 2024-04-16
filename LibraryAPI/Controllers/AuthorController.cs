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
	public class AuthorController :  ControllerBase
	{
		private readonly ILibraryServices _libraryServices;
		
		public AuthorController(ILibraryServices libraryServices)
		{
			_libraryServices = libraryServices;
		}

		[HttpGet]
		public async Task<IActionResult> GetAuthors()
		{
			var authors = await _libraryServices.GetAuthorsAsync();
			if (authors == null)
			{
				return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
			}

			return StatusCode(StatusCodes.Status200OK, authors);
		}

		[HttpGet("id")]
		public async Task<IActionResult> GetAuthor(int id, bool includeBooks = false)
		{
			Authors author = await _libraryServices.GetAuthorAsync(id, includeBooks);
			
			if (author == null)
			{
				return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
			}

			return StatusCode(StatusCodes.Status200OK, author);
		}


		[HttpPost]
		public async Task<ActionResult<Authors>> AddAuthor(Authors author)
		{
			var dbAuthor = await _libraryServices.AddAuthorAsync(author);
			
			if (dbAuthor == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"{author.FullName} could not be added.");
			}

			return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
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

