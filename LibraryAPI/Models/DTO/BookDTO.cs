﻿using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.DTO
{
	public class BookDTO
	{
		[Key]
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public bool? IsRead { get; set; }
		public DateTime? DateRead { get; set; }
		public int? Rate { get; set; }
		public string? Genre { get; set; }
		public string? CoverUrl { get; set; }
		public DateTime DateAdded { get; set; }
		public int PublisherId { get; set; }
		public string PublisherName { get; set; }
		public List<string> AuthorName { get; set; }
	}
}
