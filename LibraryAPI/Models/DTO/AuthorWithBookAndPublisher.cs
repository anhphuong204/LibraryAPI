using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.DTO
{
	public class AuthorWithBookAndPublisher
	{
		public int Id { get; set; }
		public string?  FullName { get; set; }
	}
}
