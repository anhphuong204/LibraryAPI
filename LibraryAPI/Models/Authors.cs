using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
	public class Authors
	{
		[Key]
		public int Id { get; set; }
		public string? FullName { get; set; }
		public ICollection<Book_Author> Book_Authors { get; set; } 
	}
}
