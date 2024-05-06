using LibraryAPI.Models;
using LibraryAPI.Models.DTO;

namespace LibraryAPI.Repositories
{
	public interface IPublisherRepository
	{
		List<PublisherDTO> GetAllPublishers();
		PublisherNoIdDTO GetPublisherById(int id);
		AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
		PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
		Publishers? DeletePublisherById(int id);

	}
}
