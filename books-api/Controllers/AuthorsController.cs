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
    public class AuthorsController : ControllerBase
    {
        AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody]AuthorVM author)
        {
            var _author = _authorsService.AddAuthor(author);
            return Ok(_author);
        }

        [HttpGet("get-author-with-books/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var _authors = _authorsService.GetAuthorWithBooks(id);
            return Ok(_authors);
        }
    }
}
