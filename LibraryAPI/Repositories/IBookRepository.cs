using LibraryAPI.Models;
using LibraryAPI.Models.DTO;

namespace LibraryAPI.Repositories
{
	public interface IBookRepository
	{
		List<BookWithAuthorAndPublisherDTO> GetAllBooks();
		BookWithAuthorAndPublisherDTO GetBookById(int id);
		AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
		AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
		Book? DeleteBookById(int id);
	}
}
