using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using System;
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
		public List<AuthorDTO> GellAllAuthors()
		{
			//Get Data From Database -Domain Model 
			var allAuthorsDomain = _libraryDbContext.Authors.ToList();
			//Map domain models to DTOs 
			var allAuthorDTO = new List<AuthorDTO>();
			foreach (var authorDomain in allAuthorsDomain)
			{
				allAuthorDTO.Add(new AuthorDTO()
				{
					Id = authorDomain.Id,
					FullName = authorDomain.FullName
				});
			}
			//return DTOs 
			return allAuthorDTO;
		}
		public AuthorNoIdDTO GetAuthorById(int id)
		{
			// get book Domain model from Db
			var authorWithIdDomain = _libraryDbContext.Authors.FirstOrDefault(x => x.Id ==
		   id);
			if (authorWithIdDomain == null)
			{
				return null;
			}
			//Map Domain Model to DTOs 
			var authorNoIdDTO = new AuthorNoIdDTO
			{
				FullName = authorWithIdDomain.FullName,
			};
			return authorNoIdDTO;
		}
		public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
		{
			var authorDomainModel = new Authors
			{
				FullName = addAuthorRequestDTO.FullName,
			};
			//Use Domain Model to create Author 
			_libraryDbContext.Authors.Add(authorDomainModel);
			_libraryDbContext.SaveChanges();
			return addAuthorRequestDTO;
		}
		public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
		{
			var authorDomain = _libraryDbContext.Authors.FirstOrDefault(n => n.Id == id);
			if (authorDomain != null)
			{
				authorDomain.FullName = authorNoIdDTO.FullName;
				_libraryDbContext.SaveChanges();
			}
			return authorNoIdDTO;
		}
		public Authors? DeleteAuthorById(int id)
		{
			var authorDomain = _libraryDbContext.Authors.FirstOrDefault(n => n.Id == id);
			if (authorDomain != null)
			{
				_libraryDbContext.Authors.Remove(authorDomain);
				_libraryDbContext.SaveChanges();
			}
			return null;
		}
	}
}
