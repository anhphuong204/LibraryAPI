namespace LibraryAPI.Models.DTO
{
	public class PublisherDTO
	{
		public int Id { get; set; }
		public string? FullName { get; set; }

	}
	public class PublisherNoIdDTO
	{
		public string? FullName { get; set; }
	}
	// add model to get Book and Author
	public class PublisherWithBooksAndAuthorsDTO
	{
		public string? FullName { get; set; }
		public List<BookAuthorDTO>? BookAuthors { set; get; }
	}
	public class BookAuthorDTO
	{
		public string? BookName { get; set; }
		public List<string>? BookAuthors { get; set; }
	}
}
