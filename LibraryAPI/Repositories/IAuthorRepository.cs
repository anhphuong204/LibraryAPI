using LibraryAPI.Models.DTO;
using LibraryAPI.Models;
using System.Collections.Generic;

namespace LibraryAPI.Repositories
{
	public interface IAuthorRepository
	{
		List<AuthorWithBookAndPublisher> GetAllAuthors();
		AuthorWithBookAndPublisher GetAuthorById(int id);
		AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
		AddAuthorRequestDTO UpdateAuthorById(int id, AddAuthorRequestDTO authorDTO);
		Authors DeleteAuthorById(int id);
	}
}
