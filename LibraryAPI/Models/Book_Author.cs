using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
	public class Book_Author
	{
		[Key]
		public int BookId { get; set; }
		public Book Book { get; set; }
		[Key]
		public int AuthorId { get; set; } 
		public Authors Author { get; set; }
	}
}
