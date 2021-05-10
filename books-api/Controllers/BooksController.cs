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
    public class BooksController : ControllerBase
    {
        public BooksService _bookService;

        public BooksController(BooksService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] BookVM book)
        {
            _bookService.AddBook(book);
            return Ok();
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _bookService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks() => Ok(_bookService.GetAllBooks());

        [HttpGet("get-book/{id}")]
        public IActionResult GetBook(int id) => Ok(_bookService.GetBook(id));

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookVM book)
        {
            var _book = _bookService.UpdateBookById(id, book);
            return Ok(_book);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }
    }
}
