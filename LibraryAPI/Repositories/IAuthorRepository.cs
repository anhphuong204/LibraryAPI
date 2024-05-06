using LibraryAPI.Models.DTO;
using LibraryAPI.Models;
using System.Collections.Generic;

namespace LibraryAPI.Repositories
{
	public interface IAuthorRepository
	{
		List<AuthorDTO> GellAllAuthors();
		AuthorNoIdDTO GetAuthorById(int id);
		AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
		AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO);
		Authors? DeleteAuthorById(int id);
	}
}
