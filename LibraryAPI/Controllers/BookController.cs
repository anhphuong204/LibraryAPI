using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace LibraryAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class BookController : ControllerBase
	{
		private readonly LibraryDbContext _libraryDbContext;
		private readonly IBookRepository _bookRepository;
		private readonly ILogger<BookController> _logger;

		public BookController(LibraryDbContext libraryDbContext, IBookRepository bookRepository, ILogger<BookController> logger)
		{
			_libraryDbContext = libraryDbContext;
			_bookRepository = bookRepository;
			_logger = logger;
		}

		[HttpGet("get_all_books")]
		[Authorize(Roles = "Read")]
		public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,[FromQuery] string? sortBy, [FromQuery] bool isAscending,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
		{
			_logger.LogInformation("GetAll Book Action method was invoked");
			_logger.LogWarning("This is a warnung log");
			_logger.LogError("This is a error log");
			// su dung reposity pattern 
			var allBooks = _bookRepository.GetAllBooks(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
			
			//debug
			_logger.LogInformation($"Finished GetAllBook request with data{JsonSerializer.Serialize(allBooks)}"); 

			return Ok(allBooks);
		}

		[HttpGet]
		[Route("get-book-by-id /{id}")]
		public IActionResult GetBookById([FromRoute] int id)
		{
			var bookWithIdDTO = _bookRepository.GetBookById(id);
			return Ok(bookWithIdDTO);
		}


		[HttpPost("add-book")]
		[ValidateModel]
		[Authorize(Roles = "Write")]
		public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
		{
			//validate request
			if (!ValidateAddBook(addBookRequestDTO))
			{
				return BadRequest(ModelState);
			}
			if (ModelState.IsValid)
			{
				var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
				return Ok(bookAdd);
			}
			else return BadRequest(ModelState);
		}


		[HttpPut("update-book-by-id/{id}")]
		[Authorize(Roles = "Write")]
		public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
		{
			var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
			return Ok(updateBook);
		}

		[HttpDelete("delete-book-by-id/{id}")]
		[Authorize(Roles = "Write")]
		public IActionResult DeleteBookById(int id)
		{
			var deleteBook = _bookRepository.DeleteBookById(id);
			return Ok(deleteBook);
		}

		#region private methods
		private bool ValidateAddBook(AddBookRequestDTO addBookRequestDTO)
		{
			if (addBookRequestDTO == null)
			{
				ModelState.AddModelError(nameof(addBookRequestDTO), $"Please add book data");
				return false;
			}
			if (string.IsNullOrEmpty(addBookRequestDTO.Description))
			{
				ModelState.AddModelError(nameof(addBookRequestDTO.Description), $"{nameof(addBookRequestDTO.Description)} cannot be null");
			}

			// kiem tra rating (0,5)
			if (addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
			{
				ModelState.AddModelError(nameof(addBookRequestDTO.Rate), $"{nameof(addBookRequestDTO.Rate)} cannot be less than 0 and more than 5");
			}
			if (ModelState.ErrorCount > 0)
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}

