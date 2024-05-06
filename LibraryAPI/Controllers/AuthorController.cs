using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthorController : ControllerBase
	{
		private readonly LibraryDbContext _libraryDbContext;
		private readonly IAuthorRepository _authorRepository;

		public AuthorController(LibraryDbContext libraryDbContext, IAuthorRepository authorRepository)
		{
			_libraryDbContext = libraryDbContext;
			_authorRepository = authorRepository;
		}

		[HttpGet("get_all_authors")]
		public IActionResult GetAllAuthors()
		{
			var allAuthors = _authorRepository.GetAllAuthors();
			return Ok(allAuthors);
		}

		[HttpGet("get-author-by-id/{id}")]
		public IActionResult GetAuthorById([FromRoute]int id)
		{
			var authorWithIdDTO = _authorRepository.GetAuthorById(id);
			return Ok(authorWithIdDTO);
		}
		

		[HttpPost("add-author")]
		public IActionResult AddAuthor([FromRoute] AddAuthorRequestDTO addAuthorRequestDTO)
		{
			var authorAdd = _authorRepository.AddAuthor(addAuthorRequestDTO);
			return Ok(authorAdd);
		}
		

		[HttpPut("update-author-by-id/{id}")]
		public IActionResult UpdateAuthorById(int id, [FromBody] AddAuthorRequestDTO authorDTO)
		{
			var updateAuthor = _authorRepository.UpdateAuthorById(id, authorDTO);
			return Ok(updateAuthor);
		}
	
		[HttpDelete("delete-author-by-id/{id}")]
		public IActionResult DeleteAuthorById(int id)
		{
			var deleteAuthor = _authorRepository.DeleteAuthorById(id);
			return Ok(deleteAuthor);
		}
	}
}
