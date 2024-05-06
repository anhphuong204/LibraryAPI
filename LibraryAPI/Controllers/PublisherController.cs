using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Linq;

namespace LibraryAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PublisherController : ControllerBase
	{
		private readonly LibraryDbContext _libraryDbContext;
		private readonly IPublisherRepository _publisherRepository;

		public PublisherController(LibraryDbContext libraryDbContext, IPublisherRepository publisherRepository)
		{
			_libraryDbContext = libraryDbContext;
			_publisherRepository = publisherRepository;
		}

		[HttpGet("get_all_publishers")]
		public IActionResult GetAllPublishers()
		{
			var allPublishers = _publisherRepository.GetAllPublishers();
			return Ok(allPublishers);
		}

		[HttpGet("get-publisher-by-id/{id}")]
		public IActionResult GetPublisherById([FromRoute] int id)
		{
			var publisherWithDTO  = _publisherRepository.GetPublisherById(id);
			return Ok(publisherWithDTO);
		}


		[HttpPost("add-publisher")]
		public IActionResult AddPublisher([FromRoute] AddPublisherRequestDTO addPublisherRequestDTO)
		{
			var PublisherAdd = _publisherRepository.AddPublisher(addPublisherRequestDTO);
			return Ok(PublisherAdd);
		}

		[HttpPut("update-publisher-by-id/{id}")]
		public IActionResult UpdatePublisherById(int id, [FromRoute] AddPublisherRequestDTO addPublisherRequestDTO)
		{
			var updatePublisher = _publisherRepository.UpdatePublisherById(id, addPublisherRequestDTO);
			return Ok(updatePublisher);
		}

		[HttpDelete("delete-publisher-by-id/{id}")]
		public IActionResult DeletePublisherById(int id)
		{
			var deletePublisher  = _publisherRepository.DeletePublisherById(id);
			return Ok(deletePublisher);
		}
	}
}
