using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.DTO
{
	public class AddPublisherRequestDTO
	{
		public int Id { get; set; }
		public string? FullName  {  get; set; }
	}
}
