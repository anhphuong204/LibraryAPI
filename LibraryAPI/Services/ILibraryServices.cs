using LibraryAPI.Models;

namespace LibraryAPI.Services
{
	public interface ILibraryServices
	{
		// Author Services
		Task<List<Authors>> GetAuthorsAsync(); // GET All Authors
		Task<Authors> GetAuthorAsync(int id, bool includeBooks = false); // GET Single Author
		Task<Authors> AddAuthorAsync(Authors author); // POST New Author
		Task<Authors> UpdateAuthorAsync(Authors author); // PUT Author
		Task<(bool, string)> DeleteAuthorAsync(Authors author); // DELETE Author

		// Book Services
		Task<List<Book>> GetBooksAsync(); // GET All Books
		Task<Book> GetBookAsync(int id); // Get Single Book
		Task<Book> AddBookAsync(Book book); // POST New Book
		Task<Book> UpdateBookAsync(Book book); // PUT Book
		Task<(bool, string)> DeleteBookAsync(Book book); // DELETE Book
	}
}
