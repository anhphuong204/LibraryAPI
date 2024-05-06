using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAPI.Repositories
{
	public class SQLAuthorRepository : IAuthorRepository
	{
		private readonly LibraryDbContext _libraryDbContext;

		public SQLAuthorRepository(LibraryDbContext libraryDbContext)
		{
			_libraryDbContext = libraryDbContext;
		}

		public List<AuthorWithBookAndPublisher> GetAllAuthors()
		{
			var allAuthors = _libraryDbContext.Authors
				.Select(author => new AuthorWithBookAndPublisher
				{
					FullName = author.FullName
				}).ToList();

			return allAuthors;
		}

		public AuthorWithBookAndPublisher GetAuthorById(int id)
		{
			var authorWithDomain = _libraryDbContext.Authors.FirstOrDefault(n => n.Id == id);
			if (authorWithDomain != null)
			{
				var authorWithIdDTO = new AuthorWithBookAndPublisher
				{
					FullName = authorWithDomain.FullName
				};
				return authorWithIdDTO;
			}
			return null;
		}

		public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
		{
			var authorDomainModel = new Authors 
			{
				FullName = addAuthorRequestDTO.FullName,
			};
			_libraryDbContext.Authors.Add(authorDomainModel);
			_libraryDbContext.SaveChanges();

			return addAuthorRequestDTO;
		}

		public AddAuthorRequestDTO UpdateAuthorById(int id, AddAuthorRequestDTO authorDTO)
		{
			var authorDomain = _libraryDbContext.Authors.FirstOrDefault(n => n.Id == id);
			if (authorDomain != null)
			{
				authorDomain.FullName = authorDTO.FullName;
				_libraryDbContext.SaveChanges();
			}

			return authorDTO;
		}

		public Authors DeleteAuthorById(int id)
		{
			var authorDomain = _libraryDbContext.Authors.FirstOrDefault(n => n.Id == id);
			if (authorDomain != null)
			{
				_libraryDbContext.Authors.Remove(authorDomain);
				_libraryDbContext.SaveChanges();
			}
			return authorDomain;
		}
	}
}
