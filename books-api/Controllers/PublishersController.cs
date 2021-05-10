using books_api.Data.Models;
using books_api.Data.Services;
using books_api.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        PublishersService _publishersService;
        public PublishersController(PublishersService publisherService)
        {
            _publishersService = publisherService;
        }

        [HttpPost("add-publisher")]
        public Publisher AddPublisher([FromBody] PublisherVM publisher)
        {
            var _publisher = _publishersService.AddAuthor(publisher);
            return _publisher;
        }

        [HttpGet("get-publisher-with-books/{id}")]
        public IActionResult GetPublisherWithBooks(int id)
        {
            var _publisher = _publishersService.GetPublisherWithBooks(id);
            return Ok(_publisher);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            var _publisher = _publishersService.DeletePublisherById(id);
            return Ok();
        }
    }
}
