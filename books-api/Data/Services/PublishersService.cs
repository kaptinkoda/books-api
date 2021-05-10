using books_api.Data.Models;
using books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddAuthor(PublisherVM author)
        {
            var _publisher = new Publisher
            {
                Name = author.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherWithBooks(int publisherId)
        {
            var _publisherWithBooks = _context.Publishers.Where(x => x.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM
            {
                Name = n.Name,
                BooksAuthors = n.Books.Select(n => new BookAuthorVM() 
                { 
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList()
                
            }).FirstOrDefault();

            return _publisherWithBooks;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(x => x.Id == id);
            if(_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
        }
    }
}
