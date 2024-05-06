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
		private readonly LibraryDbContext _libraryDbContext ;
		public SQLPublisherRepository(LibraryDbContext libraryDbContext)
		{
			_libraryDbContext = libraryDbContext;
		}
		public List<PublisherDTO> GetAllPublishers()
		{
			//Get Data From Database -Domain Model
			var allPublishersDomain = _libraryDbContext.Publishers.ToList();
			//Map domain models to DTOs
			var allPublisherDTO = new List<PublisherDTO>();
			foreach (var publisherDomain in allPublishersDomain)
			{
				allPublisherDTO.Add(new PublisherDTO()
				{
					Id = publisherDomain.Id,
					FullName = publisherDomain.FullName
				});
			}
			return allPublisherDTO;
		}
		public PublisherNoIdDTO GetPublisherById(int id)
		{
			// get book Domain model from Db
			var publisherWithIdDomain = _libraryDbContext.Publishers.FirstOrDefault(x => x.Id == id);
			if (publisherWithIdDomain != null)
			{ //Map Domain Model to DTOs
				var publisherNoIdDTO = new PublisherNoIdDTO
				{
					FullName = publisherWithIdDomain.FullName,
				};
				return publisherNoIdDTO;
			}
			return null;
		}
		public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
		{
			var publisherDomainModel = new Publishers 
			{
				FullName = addPublisherRequestDTO.FullName,
			};
			//Use Domain Model to create Book
			_libraryDbContext.Publishers.Add(publisherDomainModel);
			_libraryDbContext.SaveChanges();
			return addPublisherRequestDTO;
		}
		public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
		{
			var publisherDomain = _libraryDbContext.Publishers.FirstOrDefault(n => n.Id == id);
			if (publisherDomain != null)
			{
				publisherDomain.FullName = publisherNoIdDTO.FullName;
				_libraryDbContext.SaveChanges();
			}
			return null;
		}
		public Publishers? DeletePublisherById(int id)
		{
			var publisherDomain = _libraryDbContext.Publishers.FirstOrDefault(n => n.Id == id);
			if (publisherDomain != null)
			{
				_libraryDbContext.Publishers.Remove(publisherDomain);
				_libraryDbContext.SaveChanges();
			}
			return null;
		}
	}
}
