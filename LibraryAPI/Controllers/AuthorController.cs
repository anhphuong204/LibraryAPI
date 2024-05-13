using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LibraryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AuthorsController : ControllerBase
	{
		private readonly LibraryDbContext _libraryDbContext;
		private readonly IAuthorRepository _authorRepository;
		public AuthorsController(LibraryDbContext libraryDbContext, IAuthorRepository authorRepository)
		{
			_libraryDbContext = libraryDbContext;
			_authorRepository = authorRepository;
		}
		[HttpGet("get-all-author")]
		public IActionResult GetAllAuthor()
		{
			var allAuthors = _authorRepository.GellAllAuthors();
			return Ok(allAuthors);
		}
		[HttpGet("get-author-by-id/{id}")]
		public IActionResult GetAuthorById(int id)
		{
			var authorWithId = _authorRepository.GetAuthorById(id);
			return Ok(authorWithId);
		}
		[HttpPost("add - author")]
		public IActionResult AddAuthors([FromBody] AddAuthorRequestDTO
	   addAuthorRequestDTO)
		{
			var authorAdd = _authorRepository.AddAuthor(addAuthorRequestDTO);
			return Ok();
		}
		[HttpPut("update-author-by-id/{id}")]
		public IActionResult UpdateBookById(int id, [FromBody] AuthorNoIdDTO
	   authorDTO)
		{
			var authorUpdate = _authorRepository.UpdateAuthorById(id, authorDTO);
			return Ok(authorUpdate);
		}
		[HttpDelete("delete-author-by-id/{id}")]
		public IActionResult DeleteBookById(int id)
		{
			var authorDelete = _authorRepository.DeleteAuthorById(id);
			return Ok();
		}
	}
}

