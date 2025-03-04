﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
	public class Book
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
		public Publishers?  Publisher { get; set; }
		public List <Book_Author> Book_Authors { get; set; }
	}

	public enum GenreType
	{
		Fiction,
		NonFiction,
		Mystery,
		Thriller,
		Romance,
		ScienceFiction,
		Fantasy,
		Biography,
		History,
		Poetry,
		Other
	}
}
