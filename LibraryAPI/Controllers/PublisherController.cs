﻿using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Linq;

namespace LibraryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublishersController : ControllerBase
	{
		private readonly LibraryDbContext _libraryDbContext;
		private readonly IPublisherRepository _publisherRepository;
		public PublishersController(LibraryDbContext libraryDbContext, IPublisherRepository publisherRepository)
		{
			_libraryDbContext = libraryDbContext;
			_publisherRepository = publisherRepository;
		}
		[HttpGet("get-all-publisher")]
		public IActionResult GetAllPublisher()
		{
			var allPublishers = _publisherRepository.GetAllPublishers();
			return Ok(allPublishers);
		}
		[HttpGet("get-publisher-by-id")]
		public IActionResult GetPublisherById(int id)
		{
			var publisherWithId = _publisherRepository.GetPublisherById(id);
			return Ok(publisherWithId);
		}
		[HttpPost("add - publisher")]
		public IActionResult AddPublisher([FromBody] AddPublisherRequestDTO
	   addPublisherRequestDTO)
		{
			var publisherAdd = _publisherRepository.AddPublisher(addPublisherRequestDTO);
			return Ok(publisherAdd);
		}
		[HttpPut("update-publisher-by-id/{id}")]
		public IActionResult UpdatePublisherById(int id, [FromBody] PublisherNoIdDTO publisherDTO)
		{
			var publisherUpdate = _publisherRepository.UpdatePublisherById(id, publisherDTO);

			return Ok(publisherUpdate);
		}
		[HttpDelete("delete-publisher-by-id/{id}")]
		public IActionResult DeletePublisherById(int id)
		{
			var publisherDelete = _publisherRepository.DeletePublisherById(id);
			return Ok();
		}
	}

}
