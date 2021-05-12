using books_api.Data.Models;
using books_api.Data.Paging;
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

        public Publisher AddPublisher(PublisherVM author)
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

        public List<Publisher> GetAllPublishers(string orderBy, string searchString, int? pageNumber)
        {
            var _publishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "name_desc":
                        _publishers = _publishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                _publishers = _publishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            int pageSize = 2;
            _publishers = PaginatedList<Publisher>.Create(_publishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return _publishers;
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
