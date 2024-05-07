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

namespace LibraryAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		private readonly LibraryDbContext _libraryDbContext;
		private readonly IBookRepository _bookRepository;

		public BookController(LibraryDbContext libraryDbContext, IBookRepository bookRepository)
		{
			_libraryDbContext = libraryDbContext;
			_bookRepository = bookRepository;

		}

		[HttpGet("get_all_books")]
		public IActionResult GetAll()
		{
			var allBooks = _bookRepository.GetAllBooks();
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
		public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
		{
			var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
			return Ok(updateBook);
		}

		[HttpDelete("delete-book-by-id/{id}")]
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

