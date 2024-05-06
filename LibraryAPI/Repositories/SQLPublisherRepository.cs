using System;
using System.Collections.Generic;
using System.Linq;
using LibraryAPI.Data;
using LibraryAPI.Models.DTO;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories
{
	public class SQLPublisherRepository : IPublisherRepository
	{
		private readonly LibraryDbContext _libraryDbContext;

		public SQLPublisherRepository(LibraryDbContext libraryDbContext)
		{
			_libraryDbContext = libraryDbContext;
		}

		public List<AuthorWithBookAndPublisher> GetAllPublishers()
		{
			return _libraryDbContext.Publishers
				.Select(publisher => new AuthorWithBookAndPublisher
				{
					FullName = publisher.FullName
				}).ToList();
		}

		public AuthorWithBookAndPublisher GetPublisherById(int id)
		{
			var publisher = _libraryDbContext.Publishers.FirstOrDefault(p => p.Id == id);
			if (publisher != null)
			{
				return new AuthorWithBookAndPublisher { FullName = publisher.FullName };
			}
			return null; 
		}

		public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
		{
			if (addPublisherRequestDTO == null)
			{
				throw new ArgumentNullException(nameof(addPublisherRequestDTO));
			}

			var publisherDomainModel = new Publishers
			{
				FullName = addPublisherRequestDTO.FullName,
			};
			_libraryDbContext.Publishers.Add(publisherDomainModel);
			_libraryDbContext.SaveChanges();

			return addPublisherRequestDTO;
		}

		public AddPublisherRequestDTO UpdatePublisherById(int id, AddPublisherRequestDTO publisherDTO)
		{
			var publisherDomain = _libraryDbContext.Publishers.FirstOrDefault(n => n.Id == id);
			if (publisherDomain != null)
			{
				publisherDomain.FullName = publisherDTO.FullName;
				_libraryDbContext.SaveChanges();
			}

			return publisherDTO;
		}

		public Publishers DeletePublisherById(int id)
		{
			var publisherDomain = _libraryDbContext.Publishers.FirstOrDefault(n => n.Id == id);
			if (publisherDomain != null)
			{
				_libraryDbContext.Publishers.Remove(publisherDomain);
				_libraryDbContext.SaveChanges();
			}
			return publisherDomain;
		}
	}
}
