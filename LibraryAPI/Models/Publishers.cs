using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
	public class Publishers
	{
		[Key]
		public int Id { get; set; }
		public string? FullName { get; set; }
	}
}
