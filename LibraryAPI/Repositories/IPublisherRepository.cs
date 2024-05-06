using LibraryAPI.Models;
using LibraryAPI.Models.DTO;

namespace LibraryAPI.Repositories
{
	public interface IPublisherRepository
	{
		List<AuthorWithBookAndPublisher> GetAllPublishers();
		AuthorWithBookAndPublisher GetPublisherById(int id);
		AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
		AddPublisherRequestDTO UpdatePublisherById(int id, AddPublisherRequestDTO publisherDTO);
		Publishers DeletePublisherById(int id);
	}
}
