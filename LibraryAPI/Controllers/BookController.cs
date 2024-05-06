﻿using LibraryAPI.Data;
using LibraryAPI.Models.DTO;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public IActionResult GetBookById([FromRoute] int id )
		{
			var bookWithIdDTO = _bookRepository.GetBookById(id);
			return Ok(bookWithIdDTO);
		}

		[HttpPost("add-book")]
		public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
		{
			var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
			return Ok(bookAdd);
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
	}
}
