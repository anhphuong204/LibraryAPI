﻿using Microsoft.EntityFrameworkCore;
using LibraryAPI.Data;
using LibraryAPI.Models.DTO;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.Repositories
{
	public class SQLBookRepository : IBookRepository
	{
		private readonly LibraryDbContext _libraryDbContext;
		public SQLBookRepository(LibraryDbContext libraryDbContext) 
		{
			_libraryDbContext = libraryDbContext;
		}

		public List<BookWithAuthorAndPublisherDTO> GetAllBooks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)


		{
			var allBooks = _libraryDbContext.Books.Select(Books => new BookWithAuthorAndPublisherDTO()
			{
				Id = Books.Id,
				Title = Books.Title,
				Description = Books.Description,
				IsRead = Books.IsRead,
				DateRead = Books.IsRead ?? false ? Books.DateRead : null,
				Rate = Books.IsRead ?? false ? Books.Rate : null,
				Genre = Books.Genre,
				CoverUrl = Books.CoverUrl,
				PublisherName = Books.Publisher.FullName,
				AuthorName = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
			}).AsQueryable();
			//filtering
			if (string.IsNullOrWhiteSpace(filterOn) == false &&
		   string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
				{
					allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
				}
			}

			//sorting
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
				{
					allBooks = isAscending ? allBooks.OrderBy(x => x.Title) :
				   allBooks.OrderByDescending(x => x.Title);
				}
			}

			//pagination
			var skipResults = (pageNumber - 1) * pageSize;
			return allBooks.Skip(skipResults).Take(pageSize).ToList();

		}


		public BookWithAuthorAndPublisherDTO GetBookById(int id) 
		{
			var bookWithDomain = _libraryDbContext.Books.Where(n => n.Id == id);
			var bookWithIdDTO = bookWithDomain.Select(Book => new BookWithAuthorAndPublisherDTO()
				{

					Id = Book.Id,
					Title = Book.Title,
					Description = Book.Description,
					IsRead = Book.IsRead,
					DateRead = Book.IsRead ?? false ? Book.DateRead : null,
					Rate = Book.IsRead ?? false ? Book.Rate : null,
					Genre = Book.Genre,
					CoverUrl = Book.CoverUrl,
					PublisherName = Book.Publisher.FullName,
					AuthorName = Book.Book_Authors.Select(n => n.Author.FullName).ToList()
				}).FirstOrDefault();
				return bookWithIdDTO;
		}

		public AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO)
		{
			var bookDomainModel = new Book
			{
				Title = addBookRequestDTO.Title,
				Description = addBookRequestDTO.Description,
				IsRead = addBookRequestDTO.IsRead,
				DateAdded = addBookRequestDTO.DateAdded,
				Rate = addBookRequestDTO.Rate,
				Genre = addBookRequestDTO.Genre,
				CoverUrl = addBookRequestDTO.CoverUrl,
				DateRead = addBookRequestDTO.DateRead,
				PublisherId = addBookRequestDTO.PublisherId,
			};
			_libraryDbContext.Books.Add(bookDomainModel);
			_libraryDbContext.SaveChanges();

			foreach (var authorid in addBookRequestDTO.AuthorId)
			{
				var _book_author = new Book_Author()
				{
					BookId = bookDomainModel.Id,
					AuthorId = authorid
				};
				_libraryDbContext.Book_Authors.Add(_book_author);
				_libraryDbContext.SaveChanges();
			}
			return addBookRequestDTO;
		}
		
		public AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO)
		{
			var bookDomain = _libraryDbContext.Books.FirstOrDefault(n => n.Id == id);
			if (bookDomain != null) 
			{
				bookDomain.Title = bookDTO.Title;
				bookDomain.Description = bookDTO.Description;
				bookDomain.IsRead = bookDTO.IsRead;
				bookDomain.DateRead = bookDTO.DateRead;
				bookDomain.Rate = bookDTO.Rate;
				bookDomain.Genre = bookDTO.Genre;
				bookDomain.CoverUrl = bookDTO.CoverUrl;
				bookDomain.DateAdded = bookDTO.DateAdded;
				bookDomain.PublisherId = bookDTO.PublisherId;
				_libraryDbContext.SaveChanges();
			}

			var authorDomain = _libraryDbContext.Book_Authors.Where(a => a.BookId == id).ToList();
			if (authorDomain != null)
			{
				_libraryDbContext.Book_Authors.RemoveRange(authorDomain);
				_libraryDbContext.SaveChanges();
			}
			foreach (var authorid in bookDTO.AuthorId)
			{
				var _book_author = new Book_Author()
				{
					BookId = id,
					AuthorId = authorid
				};
				_libraryDbContext.Book_Authors.Add(_book_author);
				_libraryDbContext.SaveChanges();
			}
			return bookDTO;
		}
		public Book? DeleteBookById(int id)
		{
			var bookDomain = _libraryDbContext.Books.FirstOrDefault(n => n.Id == id);
			if (bookDomain != null)
			{
				_libraryDbContext.Books.Remove(bookDomain);
				_libraryDbContext.SaveChanges();
			}
			return bookDomain;
		}
	}
}

