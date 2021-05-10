using books_api.Data.Models;
using books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };

            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId,

            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AuthorIds)
            {
                var _book_author = new Book_Author
                {
                    AuthorId = id,
                    BookId = _book.Id
                };

                _context.Book_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public BookWithAuthorsVM GetBook(int bookId) {
            var _book = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _book;
        }

        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.Rate = book.Rate;
                _book.DateRead = book.DateRead;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
                
            }
            return _book;
        }

        public void DeleteBook(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(x => x.Id == bookId);

            if(_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }

        }
   
    }
}
